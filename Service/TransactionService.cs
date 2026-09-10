// using System.Data;
// using Npgsql;

// namespace MoneyFlow.Service
// {
//     public class TransactionService
//     {
//         private readonly string _con = "Server=localhost;Port=5432;Database=casepoint;User Id=postgres;Password=$hruti60;";
//         public TransactionService()
//         {
           
//         }


        
//         public DataTable GetCategories(int userId, string categoryType)
//         {
//             DataTable dt = new DataTable();

//             try
//             {
//                 using (NpgsqlConnection con =
//                     new NpgsqlConnection(_con))
//                 {
//                    string query = @"
//                         SELECT
//                             c_category_id,
//                             c_category_name,
//                             c_category_type,
//                             c_user_id
//                         FROM t_category
//                         WHERE 
//                             (c_createdBy_user_id = @UserId OR c_createdBy_user_id IS NULL)
//                             AND c_category_type = @CategoryType
//                         ORDER BY c_category_name;
//                     ";

//                     using (NpgsqlCommand cmd =
//                         new NpgsqlCommand(query, con))
//                     {
//                         cmd.Parameters.AddWithValue("@UserId", userId);
//                         cmd.Parameters.AddWithValue("@CategoryType", categoryType);

//                         using (NpgsqlDataAdapter da =
//                             new NpgsqlDataAdapter(cmd))
//                         {
//                             da.Fill(dt);
//                         }
//                     }
//                 }

//                 return dt;
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(
//                     "Error while loading categories.",
//                     ex);
//             }
//         }
//     }
// }

using System;
using Npgsql;

namespace MoneyFlow.Service
{
    public class TransactionService
    {
        
           

         private readonly string _con =Env.ConnectionString;
        public TransactionService()
        {
        }

        public void AddTransaction(
            string transactionType,
            int categoryId,
            decimal amount,
            DateTime transactionDate,
            int userId,
            string? description)
        {
            try
            {
                using (NpgsqlConnection con =
                    new NpgsqlConnection(_con))
                {
                    string query = @"
                        INSERT INTO t_transaction
                        (
                            c_transaction_type,
                            c_transaction_category_id,
                            c_transaction_amount,
                            c_transaction_date,
                            c_user_id,
                            c_transaction_description
                        )
                        VALUES
                        (
                            @TransactionType,
                            @CategoryId,
                            @Amount,
                            @TransactionDate,
                            @UserId,
                            @Description
                        );
                    ";

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@TransactionType", transactionType);
                        cmd.Parameters.AddWithValue(
                            "@CategoryId", categoryId);
                        cmd.Parameters.AddWithValue(
                            "@Amount", amount);
                        cmd.Parameters.AddWithValue(
                            "@TransactionDate", transactionDate);
                        cmd.Parameters.AddWithValue(
                            "@UserId", userId);
                        cmd.Parameters.AddWithValue(
                            "@Description", (object?)description ?? DBNull.Value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error while adding transaction: " + ex.Message,
                    ex);
            }
        }
    }
}