using MoneyFlow.Data.Queries;
using MoneyFlow.Service;
using Npgsql;

namespace MoneyFlow.Data;

public static class DatabaseInitializer
{
    private static string AdminConnectionString
    {
        get
        {
            var builder = new NpgsqlConnectionStringBuilder(Env.ConnectionString)
            {
                Database = "postgres"
            };
            return builder.ConnectionString;
        }
    }

    private static string AppConnectionString => Env.ConnectionString;

    public static void Initialize()
    {
        CreateDatabaseIfNotExists();
        CreateTables();
    }

    private static void CreateDatabaseIfNotExists()
    {
        using var connection = new NpgsqlConnection(AdminConnectionString);
        connection.Open();

        using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = """
            SELECT EXISTS (
                SELECT FROM pg_database
                WHERE datname = 'MoneyFlow'
            );
            """;

        var databaseExists = (bool)checkCommand.ExecuteScalar()!;

        if (!databaseExists)
        {
            using var createCommand = connection.CreateCommand();
            createCommand.CommandText = "CREATE DATABASE \"MoneyFlow\";";
            createCommand.ExecuteNonQuery();
        }
    }

    private static void CreateTables()
    {
        using var connection = new NpgsqlConnection(AppConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = DatabaseSchema.CreateTables;
        command.ExecuteNonQuery();
    }
}