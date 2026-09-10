using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    public class UserService
    {
        public bool UsernameExists(string username)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM t_user
                    WHERE c_user_username = @username;
                ";

                using (NpgsqlCommand command =
                       new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@username",
                        username
                    );

                    long count = Convert.ToInt64(
                        command.ExecuteScalar() ?? 0
                    );

                    return count > 0;
                }
            }
        }

        public bool RegisterUser(User user)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO t_user
                    (
                        c_user_fullname,
                        c_user_username,
                        c_user_password
                    )
                    VALUES
                    (
                        @fullname,
                        @username,
                        @password
                    );
                ";

                using (NpgsqlCommand command =
                       new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@fullname",
                        user.UserFullName!
                    );

                    command.Parameters.AddWithValue(
                        "@username",
                        user.UserUsername!
                    );

                    command.Parameters.AddWithValue(
                        "@password",
                        user.UserPassword!
                    );

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}