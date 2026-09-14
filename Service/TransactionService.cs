using System;
using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    public class TransactionService
    {
        
           

         private readonly string _con =Env.ConnectionString;
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
                    con.Open();

                    using (NpgsqlCommand sumCmd = new NpgsqlCommand(@"
                        INSERT INTO t_summary (c_user_id, c_total_income, c_total_expense)
                        VALUES (@UserId, 0, 0)
                        ON CONFLICT (c_user_id) DO NOTHING;", con))
                    {
                        sumCmd.Parameters.AddWithValue("@UserId", userId);
                        sumCmd.ExecuteNonQuery();
                    }

                    string query = @"
                        INSERT INTO t_transaction
                        (
                            c_transaction_type,
                            c_transaction_category_id,
                            c_transaction_amount,
                            c_transaction_date,
                            c_user_id,
                            c_summary_id,
                            c_transaction_description,
                            c_user_transaction_no
                        )
                        VALUES
                        (
                            @TransactionType,
                            @CategoryId,
                            @Amount,
                            @TransactionDate,
                            @UserId,
                            (SELECT c_summary_id FROM t_summary WHERE c_user_id = @UserId LIMIT 1),
                            @Description,
                            (SELECT COALESCE(MAX(c_user_transaction_no), 0) + 1 FROM t_transaction WHERE c_user_id = @UserId)
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

                        cmd.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand updSummaryCmd = new NpgsqlCommand(@"
                        UPDATE t_summary s
                        SET c_total_income = COALESCE((
                                SELECT SUM(t.c_transaction_amount)
                                FROM t_transaction t
                                WHERE t.c_user_id = @UserId AND t.c_transaction_type = 'Income'
                            ), 0),
                            c_total_expense = COALESCE((
                                SELECT SUM(t.c_transaction_amount)
                                FROM t_transaction t
                                WHERE t.c_user_id = @UserId AND t.c_transaction_type = 'Expense'
                            ), 0)
                        WHERE s.c_user_id = @UserId;", con))
                    {
                        updSummaryCmd.Parameters.AddWithValue("@UserId", userId);
                        updSummaryCmd.ExecuteNonQuery();
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
                        c_transaction_description,
                        COALESCE(c_user_transaction_no, c_transaction_id) AS c_user_transaction_no
                    FROM t_transaction
                    WHERE (c_user_transaction_no = @TransactionId OR c_transaction_id = @TransactionId)
                      AND c_user_id = @UserId
                    ORDER BY (CASE WHEN c_user_transaction_no = @TransactionId THEN 0 ELSE 1 END)
                    LIMIT 1;", con))
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
                                : reader.GetString(5),
                            UserTransactionNo = reader.IsDBNull(6) ? reader.GetInt32(0) : reader.GetInt32(6)
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
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException(
                            "The transaction does not belong to the current user.");
                    }

                    using (NpgsqlCommand updSummaryCmd = new NpgsqlCommand(@"
                        UPDATE t_summary s
                        SET c_total_income = COALESCE((
                                SELECT SUM(t.c_transaction_amount)
                                FROM t_transaction t
                                WHERE t.c_user_id = @UserId AND t.c_transaction_type = 'Income'
                            ), 0),
                            c_total_expense = COALESCE((
                                SELECT SUM(t.c_transaction_amount)
                                FROM t_transaction t
                                WHERE t.c_user_id = @UserId AND t.c_transaction_type = 'Expense'
                            ), 0)
                        WHERE s.c_user_id = @UserId;", con))
                    {
                        updSummaryCmd.Parameters.AddWithValue("@UserId", userId);
                        updSummaryCmd.ExecuteNonQuery();
                    }
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
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException(
                            "The transaction does not belong to the current user.");
                    }

                    using (NpgsqlCommand updSummaryCmd = new NpgsqlCommand(@"
                        UPDATE t_summary s
                        SET c_total_income = COALESCE((
                                SELECT SUM(t.c_transaction_amount)
                                FROM t_transaction t
                                WHERE t.c_user_id = @UserId AND t.c_transaction_type = 'Income'
                            ), 0),
                            c_total_expense = COALESCE((
                                SELECT SUM(t.c_transaction_amount)
                                FROM t_transaction t
                                WHERE t.c_user_id = @UserId AND t.c_transaction_type = 'Expense'
                            ), 0)
                        WHERE s.c_user_id = @UserId;", con))
                    {
                        updSummaryCmd.Parameters.AddWithValue("@UserId", userId);
                        updSummaryCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while deleting transaction: " + ex.Message,
                    ex);
            }
        }

        public List<TransactionModel> GetAllTransactions(int userId)
        {
            var list = new List<TransactionModel>();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    SELECT
                        t.c_transaction_id,
                        t.c_transaction_date,
                        COALESCE(c.c_category_name, 'General') AS c_category_name,
                        t.c_transaction_description,
                        t.c_transaction_amount,
                        t.c_transaction_type,
                        t.c_transaction_category_id,
                        t.c_user_id,
                        COALESCE(t.c_user_transaction_no, t.c_transaction_id) AS c_user_transaction_no
                    FROM t_transaction t
                    LEFT JOIN t_category c ON t.c_transaction_category_id = c.c_category_id
                    WHERE t.c_user_id = @UserId
                    ORDER BY t.c_transaction_date DESC, t.c_transaction_id DESC;", con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TransactionModel
                            {
                                TransactionId = reader.GetInt32(0),
                                TransactionDate = reader.GetDateTime(1),
                                CategoryName = reader.GetString(2),
                                TransactionDescription = reader.IsDBNull(3) ? null : reader.GetString(3),
                                TransactionAmount = reader.GetDecimal(4),
                                TransactionType = reader.GetString(5),
                                TransactionCategoryId = reader.GetInt32(6),
                                UserId = reader.GetInt32(7),
                                UserTransactionNo = reader.IsDBNull(8) ? reader.GetInt32(0) : reader.GetInt32(8)
                            });
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching transactions: " + ex.Message, ex);
            }
        }

        public bool TransactionExistsByDateAndCategory(int userId, DateTime transactionDate, int categoryId, string categoryName)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    SELECT EXISTS (
                        SELECT 1
                        FROM t_transaction t
                        LEFT JOIN t_category c ON t.c_transaction_category_id = c.c_category_id
                        WHERE t.c_user_id = @UserId
                          AND t.c_transaction_date = @TransactionDate
                          AND (t.c_transaction_category_id = @CategoryId OR LOWER(c.c_category_name) = LOWER(@CategoryName))
                    );", con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate.Date);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName.Trim());

                    con.Open();
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while checking transaction existence: " + ex.Message, ex);
            }
        }
    }
}