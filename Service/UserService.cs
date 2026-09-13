using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    public class UserService
    {
        public User? AuthenticateUser(string username, string password)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        c_user_id,
                        c_user_fullname,
                        c_user_username,
                        c_user_password,
                        c_user_created_at
                    FROM t_user
                    WHERE c_user_username = @username
                      AND c_user_password = @password;
                ";

                using (NpgsqlCommand command =
                       new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        int userIdOrdinal = reader.GetOrdinal("c_user_id");
                        int fullNameOrdinal = reader.GetOrdinal("c_user_fullname");
                        int usernameOrdinal = reader.GetOrdinal("c_user_username");
                        int passwordOrdinal = reader.GetOrdinal("c_user_password");
                        int createdAtOrdinal = reader.GetOrdinal("c_user_created_at");

                        return new User
                        {
                            UserId = reader.GetInt32(userIdOrdinal),
                            UserFullName = reader.IsDBNull(fullNameOrdinal) ? null : reader.GetString(fullNameOrdinal),
                            UserUsername = reader.IsDBNull(usernameOrdinal) ? null : reader.GetString(usernameOrdinal),
                            UserPassword = reader.IsDBNull(passwordOrdinal) ? null : reader.GetString(passwordOrdinal),
                            UserCreatedAt = reader.GetDateTime(createdAtOrdinal)
                        };
                    }
                }
            }
        }

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

        public bool ChangePassword(int userId, string currentPassword, string newPassword)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE t_user
                    SET c_user_password = @newPassword
                    WHERE c_user_id = @userId
                      AND c_user_password = @currentPassword;
                ";

                using (NpgsqlCommand command =
                       new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newPassword", newPassword);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@currentPassword", currentPassword);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}