using MoneyFlow.Model;
using Npgsql;

namespace MoneyFlow.Service
{
    public class SettingService
    {
        public UserSettings? GetUserSettings(int userId)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(env.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        c_setting_id,
                        c_user_id,
                        c_font_name,
                        c_font_size,
                        c_text_color,
                        c_background_color
                    FROM t_user_settings
                    WHERE c_user_id = @userId;
                ";

                using (NpgsqlCommand command =
                       new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        int settingIdOrdinal = reader.GetOrdinal("c_setting_id");
                        int userIdOrdinal = reader.GetOrdinal("c_user_id");
                        int fontNameOrdinal = reader.GetOrdinal("c_font_name");
                        int fontSizeOrdinal = reader.GetOrdinal("c_font_size");
                        int textColorOrdinal = reader.GetOrdinal("c_text_color");
                        int backgroundColorOrdinal = reader.GetOrdinal("c_background_color");

                        return new UserSettings
                        {
                            SettingId = reader.GetInt32(settingIdOrdinal),
                            UserId = reader.GetInt32(userIdOrdinal),
                            FontName = reader.IsDBNull(fontNameOrdinal) ? null : reader.GetString(fontNameOrdinal),
                            FontSize = reader.GetInt32(fontSizeOrdinal),
                            TextColor = reader.IsDBNull(textColorOrdinal) ? null : reader.GetString(textColorOrdinal),
                            BackgroundColor = reader.IsDBNull(backgroundColorOrdinal) ? null : reader.GetString(backgroundColorOrdinal)
                        };
                    }
                }
            }
        }

        public bool SaveUserSettings(UserSettings settings)
        {
            using (NpgsqlConnection connection =
                   new NpgsqlConnection(env.ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO t_user_settings
                    (
                        c_user_id,
                        c_font_name,
                        c_font_size,
                        c_text_color,
                        c_background_color
                    )
                    VALUES
                    (
                        @userId,
                        @fontName,
                        @fontSize,
                        @textColor,
                        @backgroundColor
                    )
                    ON CONFLICT (c_user_id)
                    DO UPDATE SET
                        c_font_name = EXCLUDED.c_font_name,
                        c_font_size = EXCLUDED.c_font_size,
                        c_text_color = EXCLUDED.c_text_color,
                        c_background_color = EXCLUDED.c_background_color;
                ";

                using (NpgsqlCommand command =
                       new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", settings.UserId);
                    command.Parameters.AddWithValue("@fontName", settings.FontName ?? string.Empty);
                    command.Parameters.AddWithValue("@fontSize", settings.FontSize);
                    command.Parameters.AddWithValue("@textColor", settings.TextColor ?? string.Empty);
                    command.Parameters.AddWithValue("@backgroundColor", settings.BackgroundColor ?? string.Empty);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}