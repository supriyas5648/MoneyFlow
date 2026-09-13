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

        try
        {
            using var alterCommand = connection.CreateCommand();
            alterCommand.CommandText = """
                ALTER TABLE t_transaction ADD COLUMN IF NOT EXISTS c_user_transaction_no INT;
                
                UPDATE t_transaction t
                SET c_user_transaction_no = sub.rn
                FROM (
                    SELECT c_transaction_id, ROW_NUMBER() OVER (PARTITION BY c_user_id ORDER BY c_transaction_id ASC) as rn
                    FROM t_transaction
                ) sub
                WHERE t.c_transaction_id = sub.c_transaction_id AND (t.c_user_transaction_no IS NULL OR t.c_user_transaction_no = 0);

                -- Ensure all users have a corresponding summary record
                INSERT INTO t_summary (c_user_id, c_total_income, c_total_expense)
                SELECT u.c_user_id, 0, 0
                FROM t_user u
                WHERE NOT EXISTS (SELECT 1 FROM t_summary s WHERE s.c_user_id = u.c_user_id);

                -- Link transactions to user summary record
                UPDATE t_transaction t
                SET c_summary_id = s.c_summary_id
                FROM t_summary s
                WHERE t.c_user_id = s.c_user_id AND t.c_summary_id IS NULL;

                -- Recalculate totals in t_summary
                UPDATE t_summary s
                SET c_total_income = COALESCE((
                        SELECT SUM(t.c_transaction_amount)
                        FROM t_transaction t
                        WHERE t.c_user_id = s.c_user_id AND t.c_transaction_type = 'Income'
                    ), 0),
                    c_total_expense = COALESCE((
                        SELECT SUM(t.c_transaction_amount)
                        FROM t_transaction t
                        WHERE t.c_user_id = s.c_user_id AND t.c_transaction_type = 'Expense'
                    ), 0);
            """;
            alterCommand.ExecuteNonQuery();
        }
        catch
        {
            // Ignore if migration already applied
        }
    }
}