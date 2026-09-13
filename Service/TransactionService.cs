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
                        cmd.ExecuteNonQuery();
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
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException(
                            "The transaction does not belong to the current user.");
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
                        t.c_user_id
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
                                UserId = reader.GetInt32(7)
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
    }
}