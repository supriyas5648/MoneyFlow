using MoneyFlow.Config;
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

            using (var connection = new NpgsqlConnection(DatabaseConfig.GetConnectionString()))
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
    }
}
