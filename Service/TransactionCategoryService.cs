using System;
using System.Data;
using Npgsql;

namespace MoneyFlow.Service
{
    public class TransactionCategoryService
    {
        private readonly string _con = env.ConnectionString;

        public TransactionCategoryService()
        {
        }

        public DataTable GetCategories(int userId, string categoryType)
        {
            DataTable dt = new DataTable();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    SELECT
                        c_category_id,
                        c_category_name,
                        c_category_type,
                        c_created_by_user_id AS c_user_id
                    FROM t_category
                    WHERE
                        (c_created_by_user_id = @UserId
                         OR c_created_by_user_id IS NULL)
                        AND c_category_type = @CategoryType
                    ORDER BY c_category_name;", con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CategoryType", categoryType);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while loading categories.", ex);
            }
        }

        public bool CategoryExists(
            int userId,
            string categoryName,
            string categoryType)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    SELECT COUNT(*)
                    FROM t_category
                    WHERE c_created_by_user_id = @UserId
                      AND LOWER(c_category_name) = LOWER(@CategoryName)
                      AND c_category_type = @CategoryType;", con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@CategoryType", categoryType);

                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while checking category.", ex);
            }
        }

        public int AddCategory(
            string categoryName,
            string categoryType,
            int userId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
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
                    RETURNING c_category_id;", con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@CategoryType", categoryType);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding category.", ex);
            }
        }
    }
}