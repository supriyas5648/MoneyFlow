using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Npgsql;

namespace MoneyFlow.Service
{
    public class FileService
    {
        private readonly string _connectionString = env.ConnectionString;

        public DataTable ReadCsv(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            string? headerLine = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new InvalidDataException("The CSV file is empty.");
            }

            List<string> headers = ParseCsvLine(headerLine);
            string[] requiredHeaders =
            {
                "Transaction ID",
                "Type",
                "Category",
                "Amount",
                "Date",
                "User ID",
                "Description"
            };

            foreach (string requiredHeader in requiredHeaders)
            {
                if (!headers.Contains(requiredHeader, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidDataException(
                        $"The CSV file must contain a '{requiredHeader}' column.");
                }
            }

            DataTable records = new DataTable();
            foreach (string header in requiredHeaders)
            {
                records.Columns.Add(header);
            }

            string? line;
            int lineNumber = 1;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                List<string> values = ParseCsvLine(line);
                if (values.Count != headers.Count)
                {
                    throw new InvalidDataException(
                        $"CSV line {lineNumber} has {values.Count} values, but {headers.Count} were expected.");
                }

                DataRow row = records.NewRow();
                foreach (string requiredHeader in requiredHeaders)
                {
                    int sourceIndex = headers.FindIndex(
                        header => string.Equals(
                            header,
                            requiredHeader,
                            StringComparison.OrdinalIgnoreCase));
                    row[requiredHeader] = values[sourceIndex].Trim();
                }

                records.Rows.Add(row);
            }

            return records;
        }

        public List<string> ValidateRecords(DataTable records, int loggedInUserId)
        {
            List<string> errors = new List<string>();
            HashSet<int> transactionIdsInFile = new HashSet<int>();

            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            foreach (DataRow row in records.Rows)
            {
                int rowNumber = records.Rows.IndexOf(row) + 2;
                string transactionType = row["Type"].ToString()?.Trim() ?? string.Empty;
                string categoryName = row["Category"].ToString()?.Trim() ?? string.Empty;
                string amountText = row["Amount"].ToString()?.Trim() ?? string.Empty;
                string dateText = row["Date"].ToString()?.Trim() ?? string.Empty;
                string description = row["Description"].ToString()?? string.Empty;
                string transactionIdText = row["Transaction ID"].ToString()?.Trim() ?? string.Empty;
                string userIdText = row["User ID"].ToString()?.Trim() ?? string.Empty;

                if (!string.Equals(transactionType, "Income", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(transactionType, "Expense", StringComparison.OrdinalIgnoreCase))
                {
                    errors.Add($"Line {rowNumber}: Type must be Income or Expense.");
                }

                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    errors.Add($"Line {rowNumber}: Category is required.");
                }
                else if (!IsValidCategoryName(categoryName))
                {
                    errors.Add($"Line {rowNumber}: Category name can contain only letters and spaces.");
                }

                if (!decimal.TryParse(amountText, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount) || amount <= 0)
                {
                    errors.Add($"Line {rowNumber}: Amount must be a positive number.");
                }

                if (!DateTime.TryParse(dateText, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    errors.Add($"Line {rowNumber}: Date is invalid.");
                }

                if (!int.TryParse(userIdText, out int fileUserId) || fileUserId != loggedInUserId)
                {
                    errors.Add($"Line {rowNumber}: User ID must match the logged-in user.");
                }

                if (!string.IsNullOrWhiteSpace(transactionIdText) &&
                    (!int.TryParse(transactionIdText, out int transactionId) || transactionId <= 0))
                {
                    errors.Add($"Line {rowNumber}: Transaction ID is invalid.");
                }
                else if (int.TryParse(transactionIdText, out int duplicateCheckId) &&
                         !transactionIdsInFile.Add(duplicateCheckId))
                {
                    errors.Add($"Line {rowNumber}: Transaction ID appears more than once in the CSV.");
                }

                if (!string.IsNullOrWhiteSpace(categoryName) &&
                    (string.Equals(transactionType, "Income", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(transactionType, "Expense", StringComparison.OrdinalIgnoreCase)))
                {
                    using NpgsqlCommand categoryCommand = new NpgsqlCommand(@"
                        SELECT COUNT(*)
                        FROM t_category
                        WHERE LOWER(c_category_name) = LOWER(@CategoryName)
                          AND c_category_type = @CategoryType
                                                    AND c_created_by_user_id = @UserId;", connection);
                    categoryCommand.Parameters.AddWithValue("@CategoryName", categoryName);
                    categoryCommand.Parameters.AddWithValue("@CategoryType", transactionType);
                    categoryCommand.Parameters.AddWithValue("@UserId", loggedInUserId);

                    // A missing category is allowed here because SaveToDatabase
                    // creates it for the logged-in user before inserting the transaction.
                }

                if (int.TryParse(transactionIdText, out int existingTransactionId))
                {
                    using NpgsqlCommand transactionCommand = new NpgsqlCommand(@"
                        SELECT c_user_id
                        FROM t_transaction
                        WHERE c_transaction_id = @TransactionId;", connection);
                    transactionCommand.Parameters.AddWithValue("@TransactionId", existingTransactionId);
                    object? existingUserId = transactionCommand.ExecuteScalar();

                    if (existingUserId != null && existingUserId != DBNull.Value &&
                        Convert.ToInt32(existingUserId) != loggedInUserId)
                    {
                        errors.Add($"Line {rowNumber}: Transaction ID belongs to another user.");
                    }
                }
            }

            return errors;
        }

        public int SaveToDatabase(DataTable records, int loggedInUserId, out int updatedCount)
        {
            int savedCount = 0;
            updatedCount = 0;

            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            using NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (DataRow row in records.Rows)
                {
                    string transactionIdText = row["Transaction ID"].ToString()?.Trim() ?? string.Empty;
                    string transactionType = NormalizeTransactionType(
                        row["Type"].ToString()!.Trim());
                    using NpgsqlCommand categoryCommand = new NpgsqlCommand(@"
                        SELECT c_category_id
                        FROM t_category
                        WHERE LOWER(c_category_name) = LOWER(@CategoryName)
                          AND c_category_type = @CategoryType
                                                    AND c_created_by_user_id = @UserId
                        LIMIT 1;", connection, transaction);
                    categoryCommand.Parameters.AddWithValue("@CategoryName", row["Category"].ToString()!.Trim());
                    categoryCommand.Parameters.AddWithValue("@CategoryType", transactionType);
                    categoryCommand.Parameters.AddWithValue("@UserId", loggedInUserId);

                    object? categoryId = categoryCommand.ExecuteScalar();
                    if (categoryId == null || categoryId == DBNull.Value)
                    {
                        using NpgsqlCommand addCategoryCommand = new NpgsqlCommand(@"
                            INSERT INTO t_category
                            (
                                c_category_name,
                                c_category_type,
                                c_created_by_user_id
                            )
                            VALUES
                            (
                                @CategoryName,
                                @CategoryType,
                                @UserId
                            )
                            RETURNING c_category_id;", connection, transaction);
                        addCategoryCommand.Parameters.AddWithValue(
                            "@CategoryName",
                            row["Category"].ToString()!.Trim());
                        addCategoryCommand.Parameters.AddWithValue(
                            "@CategoryType",
                            transactionType);
                        addCategoryCommand.Parameters.AddWithValue(
                            "@UserId",
                            loggedInUserId);

                        categoryId = addCategoryCommand.ExecuteScalar();
                    }

                    if (int.TryParse(transactionIdText, out int transactionId))
                    {
                        using NpgsqlCommand existingCommand = new NpgsqlCommand(@"
                            SELECT COUNT(*)
                            FROM t_transaction
                            WHERE c_transaction_id = @TransactionId
                              AND c_user_id = @UserId;", connection, transaction);
                        existingCommand.Parameters.AddWithValue("@TransactionId", transactionId);
                        existingCommand.Parameters.AddWithValue("@UserId", loggedInUserId);

                        if (Convert.ToInt32(existingCommand.ExecuteScalar()) > 0)
                        {
                            using NpgsqlCommand updateCommand = new NpgsqlCommand(@"
                                UPDATE t_transaction
                                SET c_transaction_type = @TransactionType,
                                    c_transaction_category_id = @CategoryId,
                                    c_transaction_amount = @Amount,
                                    c_transaction_date = @TransactionDate,
                                    c_transaction_description = @Description,
                                    c_transaction_updated_at = CURRENT_TIMESTAMP
                                WHERE c_transaction_id = @TransactionId
                                  AND c_user_id = @UserId
                                  AND (
                                      c_transaction_type IS DISTINCT FROM @TransactionType
                                      OR c_transaction_category_id IS DISTINCT FROM @CategoryId
                                      OR c_transaction_amount IS DISTINCT FROM @Amount
                                      OR c_transaction_date IS DISTINCT FROM @TransactionDate
                                      OR c_transaction_description IS DISTINCT FROM @Description
                                  );", connection, transaction);
                            updateCommand.Parameters.AddWithValue("@TransactionId", transactionId);
                            updateCommand.Parameters.AddWithValue("@TransactionType", transactionType);
                            updateCommand.Parameters.AddWithValue("@CategoryId", Convert.ToInt32(categoryId));
                            updateCommand.Parameters.AddWithValue("@Amount", decimal.Parse(row["Amount"].ToString()!, CultureInfo.InvariantCulture));
                            updateCommand.Parameters.AddWithValue("@TransactionDate", DateTime.Parse(row["Date"].ToString()!, CultureInfo.InvariantCulture));
                            updateCommand.Parameters.AddWithValue("@UserId", loggedInUserId);
                            updateCommand.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(row["Description"].ToString())
                                ? DBNull.Value
                                : row["Description"].ToString()!.Trim());

                            if (updateCommand.ExecuteNonQuery() > 0)
                            {
                                updatedCount++;
                            }

                            continue;
                        }
                    }

                    using NpgsqlCommand insertCommand = new NpgsqlCommand(@"
                        INSERT INTO t_transaction
                        (
                            c_transaction_type,
                            c_transaction_category_id,
                            c_transaction_amount,
                            c_transaction_date,
                            c_user_id,
                            c_transaction_description
                        )
                        VALUES
                        (
                            @TransactionType,
                            @CategoryId,
                            @Amount,
                            @TransactionDate,
                            @UserId,
                            @Description
                        );", connection, transaction);

                    insertCommand.Parameters.AddWithValue("@TransactionType", transactionType);

                    insertCommand.Parameters.AddWithValue("@CategoryId", Convert.ToInt32(categoryId));
                    insertCommand.Parameters.AddWithValue("@Amount", decimal.Parse(row["Amount"].ToString()!, CultureInfo.InvariantCulture));
                    insertCommand.Parameters.AddWithValue("@TransactionDate", DateTime.Parse(row["Date"].ToString()!, CultureInfo.InvariantCulture));
                    insertCommand.Parameters.AddWithValue("@UserId", loggedInUserId);
                    insertCommand.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(row["Description"].ToString())
                        ? DBNull.Value
                        : row["Description"].ToString()!.Trim());
                    insertCommand.ExecuteNonQuery();
                    savedCount++;
                }

                using NpgsqlCommand summaryCommand = new NpgsqlCommand(@"
                    INSERT INTO t_summary (c_user_id)
                    VALUES (@UserId)
                    ON CONFLICT (c_user_id) DO NOTHING;

                    UPDATE t_summary
                    SET c_total_income = COALESCE((
                            SELECT SUM(c_transaction_amount)
                            FROM t_transaction
                            WHERE c_user_id = @UserId
                              AND c_transaction_type = 'Income'), 0),
                        c_total_expense = COALESCE((
                            SELECT SUM(c_transaction_amount)
                            FROM t_transaction
                            WHERE c_user_id = @UserId
                              AND c_transaction_type = 'Expense'), 0)
                    WHERE c_user_id = @UserId;", connection, transaction);
                summaryCommand.Parameters.AddWithValue("@UserId", loggedInUserId);
                summaryCommand.ExecuteNonQuery();

                transaction.Commit();
                return savedCount;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void ExportToCsv(DataTable records, string filePath)
        {
            using StreamWriter writer = new StreamWriter(filePath);
            string[] headers =
            {
                "Transaction ID",
                "Type",
                "Category",
                "Amount",
                "Date",
                "User ID",
                "Description"
            };

            writer.WriteLine(string.Join(",", headers));
            foreach (DataRow row in records.Rows)
            {
                writer.WriteLine(string.Join(",", new[]
                {
                    EscapeCsvValue(row["Transaction ID"]),
                    EscapeCsvValue(row["Type"]),
                    EscapeCsvValue(row["Category"]),
                    EscapeCsvValue(row["Amount"]),
                    EscapeCsvValue(row["Date"]),
                    EscapeCsvValue(row["User ID"]),
                    EscapeCsvValue(row["Description"])
                }));
            }
        }

        private static string EscapeCsvValue(object value)
        {
            string text = value == DBNull.Value ? string.Empty : Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
            return $"\"{text.Replace("\"", "\"\"")}\"";
        }

        private static bool IsValidCategoryName(string categoryName)
        {
            if (categoryName.Length > 100)
            {
                return false;
            }

            foreach (char character in categoryName)
            {
                if (!char.IsLetter(character) && character != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private static string NormalizeTransactionType(string transactionType)
        {
            return string.Equals(transactionType, "Income", StringComparison.OrdinalIgnoreCase)
                ? "Income"
                : "Expense";
        }

        private static List<string> ParseCsvLine(string line)
        {
            List<string> values = new List<string>();
            string currentValue = string.Empty;
            bool insideQuotes = false;

            for (int index = 0; index < line.Length; index++)
            {
                char character = line[index];
                if (character == '"')
                {
                    if (insideQuotes && index + 1 < line.Length && line[index + 1] == '"')
                    {
                        currentValue += '"';
                        index++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (character == ',' && !insideQuotes)
                {
                    values.Add(currentValue);
                    currentValue = string.Empty;
                }
                else
                {
                    currentValue += character;
                }
            }

            if (insideQuotes)
            {
                throw new InvalidDataException("The CSV file contains an unclosed quoted value.");
            }

            values.Add(currentValue);
            return values;
        }
    }
}
