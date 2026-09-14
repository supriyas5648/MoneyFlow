using System.Collections.Generic;
using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    /// <summary>
    /// Service class for category-related database operations on t_category.
    /// </summary>
    public class CategoryService
    {
        /// <summary>
        /// Fetches all categories for a given user (system categories + user-created).
        /// System categories have c_created_by_user_id = NULL.
        /// </summary>
        public List<CategoryModel> GetAllCategories(int userId)
        {
            var categories = new List<CategoryModel>();

            string query = @"
                SELECT DISTINCT ON (LOWER(c_category_name))
                    c_category_id, c_category_name, c_category_type, c_created_by_user_id
                FROM t_category
                WHERE c_created_by_user_id IS NULL OR c_created_by_user_id = @userId
                ORDER BY LOWER(c_category_name), (c_created_by_user_id IS NULL), c_category_id";

            using (var connection = new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new CategoryModel
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1),
                                CategoryType = reader.GetString(2),
                                CreatedByUserId = reader.IsDBNull(3) ? null : reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return categories;
        }
    }
}
