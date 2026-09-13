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
                    )
                    RETURNING c_user_id;
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

                    object? result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int newUserId))
                    {
                        user.UserId = newUserId;

                        using (var summaryCmd = new NpgsqlCommand(@"
                            INSERT INTO t_summary
                            (
                                c_user_id,
                                c_total_income,
                                c_total_expense
                            )
                            VALUES
                            (
                                @UserId,
                                0,
                                0
                            )
                            ON CONFLICT (c_user_id) DO NOTHING;", connection))
                        {
                            summaryCmd.Parameters.AddWithValue("@UserId", newUserId);
                            summaryCmd.ExecuteNonQuery();
                        }

                        return true;
                    }

                    return false;
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

        public bool DeleteUser(int userId)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(Env.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.Parameters.AddWithValue("@userId", userId);

                            // 1. Delete user transactions
                            cmd.CommandText = "DELETE FROM t_transaction WHERE c_user_id = @userId;";
                            cmd.ExecuteNonQuery();

                            // 2. Delete user settings
                            cmd.CommandText = "DELETE FROM t_user_settings WHERE c_user_id = @userId;";
                            cmd.ExecuteNonQuery();

                            // 3. Delete user summary
                            cmd.CommandText = "DELETE FROM t_summary WHERE c_user_id = @userId;";
                            cmd.ExecuteNonQuery();

                            // 4. Delete user custom categories
                            cmd.CommandText = "DELETE FROM t_category WHERE c_created_by_user_id = @userId;";
                            cmd.ExecuteNonQuery();

                            // 5. Delete user
                            cmd.CommandText = "DELETE FROM t_user WHERE c_user_id = @userId;";
                            int rows = cmd.ExecuteNonQuery();

                            transaction.Commit();
                            return rows > 0;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}