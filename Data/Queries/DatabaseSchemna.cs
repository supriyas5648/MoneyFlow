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
            c_created_by_user_id INT NULL,
            c_category_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

            CONSTRAINT fk_category_created_by_user
                FOREIGN KEY (c_created_by_user_id)
                REFERENCES t_user(c_user_id)
                ON DELETE CASCADE,

            CONSTRAINT chk_category_type
                CHECK (c_category_type IN ('Income', 'Expense'))
        );

        CREATE TABLE IF NOT EXISTS t_summary
        (
            c_summary_id SERIAL PRIMARY KEY,
            c_user_id INT NOT NULL,
            c_total_income NUMERIC(12,2) NOT NULL DEFAULT 0,
            c_total_expense NUMERIC(12,2) NOT NULL DEFAULT 0,
            c_summary_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

            CONSTRAINT uq_summary_user
                UNIQUE (c_user_id),

            CONSTRAINT fk_summary_user
                FOREIGN KEY (c_user_id)
                REFERENCES t_user(c_user_id)
                ON DELETE CASCADE,

            CONSTRAINT chk_summary_income
                CHECK (c_total_income >= 0),

            CONSTRAINT chk_summary_expense
                CHECK (c_total_expense >= 0)
        );

        CREATE TABLE IF NOT EXISTS t_transaction
        (
            c_transaction_id SERIAL PRIMARY KEY,
            c_transaction_type VARCHAR(20) NOT NULL,
            c_transaction_category_id INT NOT NULL,
            c_transaction_amount NUMERIC(12,2) NOT NULL,
            c_transaction_description VARCHAR(255),
            c_transaction_date DATE NOT NULL,
            c_summary_id INT NOT NULL,
            c_user_id INT NOT NULL,
            c_transaction_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
            c_transaction_updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

            CONSTRAINT fk_transaction_category
                FOREIGN KEY (c_transaction_category_id)
                REFERENCES t_category(c_category_id),

            CONSTRAINT fk_transaction_summary
                FOREIGN KEY (c_summary_id)
                REFERENCES t_summary(c_summary_id),

            CONSTRAINT fk_transaction_user
                FOREIGN KEY (c_user_id)
                REFERENCES t_user(c_user_id)
                ON DELETE CASCADE,

            CONSTRAINT chk_transaction_type
                CHECK (c_transaction_type IN ('Income', 'Expense')),

            CONSTRAINT chk_transaction_amount
                CHECK (c_transaction_amount > 0)
        );

        CREATE TABLE IF NOT EXISTS t_user_settings
        (
            c_setting_id SERIAL PRIMARY KEY,
            c_user_id INT NOT NULL UNIQUE,
            c_font_name VARCHAR(100) NOT NULL DEFAULT 'Arial',
            c_font_size INT NOT NULL DEFAULT 10,
            c_text_color VARCHAR(20) NOT NULL DEFAULT '#000000',
            c_background_color VARCHAR(20) NOT NULL DEFAULT '#FFFFFF',

            CONSTRAINT fk_user_settings_user
                FOREIGN KEY (c_user_id)
                REFERENCES t_user(c_user_id)
                ON DELETE CASCADE,

            CONSTRAINT chk_font_size
                CHECK (c_font_size BETWEEN 6 AND 72)
        );
        """;
}