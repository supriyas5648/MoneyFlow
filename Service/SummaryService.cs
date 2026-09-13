using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    /// <summary>
    /// Service class for summary-related database operations on t_summary.
    /// </summary>
    public class SummaryService
    {
        /// <summary>
        /// Fetches the financial summary (total income and expense) for a given user.
        /// Returns null if no summary row exists for the user.
        /// </summary>
        public SummaryModel? GetSummary(int userId)
        {
            string query = @"
                SELECT c_summary_id, c_user_id, c_total_income, c_total_expense
                FROM t_summary
                WHERE c_user_id = @userId";

            using (var connection = new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SummaryModel
                            {
                                SummaryId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                TotalIncome = reader.GetDecimal(2),
                                TotalExpense = reader.GetDecimal(3)
                            };
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Ensures a summary row exists for the given user in t_summary.
        /// </summary>
        public void EnsureSummaryExists(int userId, NpgsqlConnection connection, NpgsqlTransaction? transaction = null)
        {
            using var cmd = new NpgsqlCommand(@"
                INSERT INTO t_summary (c_user_id, c_total_income, c_total_expense)
                VALUES (@UserId, 0, 0)
                ON CONFLICT (c_user_id) DO NOTHING;", connection, transaction);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Recalculates and updates the total income and total expense in t_summary for the given user.
        /// </summary>
        public void UpdateSummaryTotals(int userId, NpgsqlConnection connection, NpgsqlTransaction? transaction = null)
        {
            using var cmd = new NpgsqlCommand(@"
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
                WHERE s.c_user_id = @UserId;", connection, transaction);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.ExecuteNonQuery();
        }
    }
}
