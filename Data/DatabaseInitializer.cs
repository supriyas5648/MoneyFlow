using MoneyFlow.Data.Queries;
using Npgsql;
using MoneyFlow.Service;

namespace MoneyFlow.Data;

public static class DatabaseInitializer
{
    private const string AdminConnectionString =
        "Host=localhost;Port=5432;Username=postgres;Password=97141;Database=postgres";

    private const string AppConnectionString =
        "Host=localhost;Port=5432;Username=postgres;Password=97141;Database=MoneyFlow";

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