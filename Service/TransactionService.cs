using System;
using System.Collections.Generic;
using MoneyFlow.Config;
using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    /// <summary>
    /// Service class for transaction-related database operations on t_transaction.
    /// </summary>
    public class TransactionService
    {
        /// <summary>
        /// Fetches all transactions for a given user, joined with t_category to get the category name.
        /// Ordered by transaction date descending (newest first).
        /// </summary>
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

            using (var connection = new NpgsqlConnection(DatabaseConfig.GetConnectionString()))
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
