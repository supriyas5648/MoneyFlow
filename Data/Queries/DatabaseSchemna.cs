namespace MoneyFlow.Data.Queries;

public static class DatabaseSchema
{
    public const string CreateTables = """
        CREATE TABLE IF NOT EXISTS t_user
        (
            c_user_id SERIAL PRIMARY KEY,
            c_user_fullname VARCHAR(100) NOT NULL,
            c_user_username VARCHAR(50) NOT NULL UNIQUE,
            c_user_password VARCHAR(255) NOT NULL,
            c_user_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        );

        CREATE TABLE IF NOT EXISTS t_category
        (
            c_category_id SERIAL PRIMARY KEY,
            c_category_name VARCHAR(100) NOT NULL,
            c_category_type VARCHAR(20) NOT NULL,
            c_user_id INT NOT NULL,

            CONSTRAINT fk_t_category_user
                FOREIGN KEY (c_user_id)
                REFERENCES t_user(c_user_id),

            CONSTRAINT chk_t_category_type
                CHECK (c_category_type IN ('Income', 'Expense'))
        );

        CREATE TABLE IF NOT EXISTS t_summary
        (
            c_summary_id SERIAL PRIMARY KEY,
            c_total_income NUMERIC(12,2) NOT NULL DEFAULT 0,
            c_total_expense NUMERIC(12,2) NOT NULL DEFAULT 0,
            c_summary_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

            CONSTRAINT chk_t_summary_income
                CHECK (c_total_income >= 0),

            CONSTRAINT chk_t_summary_expense
                CHECK (c_total_expense >= 0)
        );

        CREATE TABLE IF NOT EXISTS t_transaction
        (
            c_transaction_id SERIAL PRIMARY KEY,
            c_transaction_type VARCHAR(20) NOT NULL,
            c_transaction_category_id INT NOT NULL,
            c_transaction_amount NUMERIC(12,2) NOT NULL,
            c_transaction_date DATE NOT NULL,
            c_summary_id INT NOT NULL,
            c_user_id INT NOT NULL,
            c_transaction_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
            c_transaction_updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

            CONSTRAINT fk_t_transaction_category
                FOREIGN KEY (c_transaction_category_id)
                REFERENCES t_category(c_category_id),

            CONSTRAINT fk_t_transaction_summary
                FOREIGN KEY (c_summary_id)
                REFERENCES t_summary(c_summary_id),

            CONSTRAINT fk_t_transaction_user
                FOREIGN KEY (c_user_id)
                REFERENCES t_user(c_user_id),

            CONSTRAINT chk_t_transaction_type
                CHECK (c_transaction_type IN ('Income', 'Expense')),

            CONSTRAINT chk_t_transaction_amount
                CHECK (c_transaction_amount > 0)
        );
        """;
}