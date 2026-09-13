using System;
using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    public class TransactionService
    {
        
           

         private readonly string _con =env.ConnectionString;
        public TransactionService()
        {
        }

        public void AddTransaction(
            string transactionType,
            int categoryId,
            decimal amount,
            DateTime transactionDate,
            int userId,
            string? description)
        {
            try
            {
                using (NpgsqlConnection con =
                    new NpgsqlConnection(_con))
                {
                    string query = @"
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
                        );
                    ";

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@TransactionType", transactionType);
                        cmd.Parameters.AddWithValue(
                            "@CategoryId", categoryId);
                        cmd.Parameters.AddWithValue(
                            "@Amount", amount);
                        cmd.Parameters.AddWithValue(
                            "@TransactionDate", transactionDate);
                        cmd.Parameters.AddWithValue(
                            "@UserId", userId);
                        cmd.Parameters.AddWithValue(
                            "@Description", (object?)description ?? DBNull.Value);

                        con.Open();
                        using NpgsqlTransaction transaction = con.BeginTransaction();
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                        UpdateSummary(con, transaction, userId);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while adding transaction: " + ex.Message,
                    ex);
            }
        }

        public Transaction? GetTransactionById(int transactionId, int userId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    SELECT
                        c_transaction_id,
                        c_transaction_type,
                        c_transaction_category_id,
                        c_transaction_amount,
                        c_transaction_date,
                        c_transaction_description
                    FROM t_transaction
                    WHERE c_transaction_id = @TransactionId
                      AND c_user_id = @UserId;", con))
                {
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Transaction
                        {
                            TransactionId = reader.GetInt32(0),
                            TransactionType = reader.GetString(1),
                            TransactionCategoryId = reader.GetInt32(2),
                            TransactionAmount = reader.GetDecimal(3),
                            TransactionDate = reader.GetDateTime(4),
                            TransactionDescription = reader.IsDBNull(5)
                                ? null
                                : reader.GetString(5)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while loading transaction: " + ex.Message,
                    ex);
            }
        }

        public bool TransactionBelongsToUser(int transactionId, int userId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    SELECT EXISTS (
                        SELECT 1
                        FROM t_transaction
                        WHERE c_transaction_id = @TransactionId
                          AND c_user_id = @UserId
                    );", con))
                {
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while validating transaction ownership: " + ex.Message,
                    ex);
            }
        }

        public void UpdateTransaction(
            int transactionId,
            string transactionType,
            int categoryId,
            decimal amount,
            DateTime transactionDate,
            int userId,
            string? description)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    UPDATE t_transaction
                    SET c_transaction_type = @TransactionType,
                        c_transaction_category_id = @CategoryId,
                        c_transaction_amount = @Amount,
                        c_transaction_date = @TransactionDate,
                        c_transaction_description = @Description
                    WHERE c_transaction_id = @TransactionId
                      AND c_user_id = @UserId;", con))
                {
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                    cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Description", (object?)description ?? DBNull.Value);

                    con.Open();
                    using NpgsqlTransaction transaction = con.BeginTransaction();
                    cmd.Transaction = transaction;
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException(
                            "The transaction does not belong to the current user.");
                    }

                            UpdateSummary(con, transaction, userId);
                            transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while updating transaction: " + ex.Message,
                    ex);
            }
        }

        public void DeleteTransaction(int transactionId, int userId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    DELETE FROM t_transaction
                    WHERE c_transaction_id = @TransactionId
                      AND c_user_id = @UserId;", con))
                {
                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    using NpgsqlTransaction transaction = con.BeginTransaction();
                    cmd.Transaction = transaction;
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException(
                            "The transaction does not belong to the current user.");
                    }

                            UpdateSummary(con, transaction, userId);
                            transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while deleting transaction: " + ex.Message,
                    ex);
            }
        }

        private static void UpdateSummary(
            NpgsqlConnection connection,
            NpgsqlTransaction transaction,
            int userId)
        {
            using NpgsqlCommand command = new NpgsqlCommand(@"
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
                WHERE c_user_id = @UserId;", connection)
            {
                Transaction = transaction
            };

            command.Parameters.AddWithValue("@UserId", userId);
            command.ExecuteNonQuery();
        }

          public List<TransactionModel> GetAllTransactions(int userId)
        {
            var transactions = new List<TransactionModel>();

            string query = @"
                SELECT 
                    t.c_transaction_id,
                    t.c_transaction_type,
                    t.c_transaction_category_id,
                    c.c_category_name,
                    t.c_transaction_amount,
                    t.c_transaction_description,
                    t.c_transaction_date,
                    t.c_summary_id,
                    t.c_user_id
                FROM t_transaction t
                INNER JOIN t_category c ON t.c_transaction_category_id = c.c_category_id
                WHERE t.c_user_id = @userId
                ORDER BY t.c_transaction_date DESC";

            using (var connection = new NpgsqlConnection(env.ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new TransactionModel
                            {
                                TransactionId = reader.GetInt32(0),
                                TransactionType = reader.GetString(1),
                                TransactionCategoryId = reader.GetInt32(2),
                                CategoryName = reader.GetString(3),
                                TransactionAmount = reader.GetDecimal(4),
                                TransactionDescription = reader.IsDBNull(5) ? null : reader.GetString(5),
                                TransactionDate = reader.GetDateTime(6),
                                SummaryId = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                                UserId = reader.GetInt32(8)
                            });
                        }
                    }
                }
            }

            return transactions;
        }

    }
}