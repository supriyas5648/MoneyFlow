namespace MoneyFlow.Config
{
    /// <summary>
    /// Centralized database configuration for PostgreSQL connection.
    /// Update the connection string below with your PostgreSQL credentials.
    /// </summary>
    public static class DatabaseConfig
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=MoneyFlow;Username=postgres;Password=root";

        /// <summary>
        /// Returns the PostgreSQL connection string.
        /// </summary>
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
