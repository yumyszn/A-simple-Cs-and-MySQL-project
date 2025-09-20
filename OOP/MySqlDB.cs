using System;
using MySql.Data.MySqlClient;

class MySqlDB
{
    #region 
    public static void EnsureDatabaseAndTable(string server, string user, string password, string database)
    {

        string serverConnection = $"server={server};user={user};password={password};";

        using (MySqlConnection conn = new MySqlConnection(serverConnection))
        {
            conn.Open();

            string createDb = $"CREATE DATABASE IF NOT EXISTS {database};";
            using (MySqlCommand cmd = new MySqlCommand(createDb, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Database '{database}' ready.");
            }
        }

        string dbConnection = $"server={server};user={user};password={password};database={database};";

        using (MySqlConnection conn = new MySqlConnection(dbConnection))
        {
            conn.Open();

            string createTable = @"
            CREATE TABLE IF NOT EXISTS Products (
                ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                Name VARCHAR(100) NOT NULL,
                Stock INT NOT NULL
            );";

            using (MySqlCommand cmd = new MySqlCommand(createTable, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table products ready.");
            }
        }
    }
    #endregion



    #region Add Method from Database
    public static void AddProductToDatabase(Product product, string connectionString)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = "INSERT INTO Products (Name, Stock) VALUES (@Name, @Stock)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // cmd.Parameters.AddWithValue("@ID", product.ID);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);


                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("The product has been added to the database!");
                    long lastId = cmd.LastInsertedId;
                    product.ID = (int)lastId;

                    Console.WriteLine($"ID: {product.ID}, İsim: {product.Name}, Stok: {product.Stock}");
                }
                else
                    Console.WriteLine("The product could not be added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);


            }
        }
    }
    #endregion

    #region Remove Method from Database

    public static void RemoveProductToDatabase(Product product, string connectionString)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = "DELETE FROM products WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ID", product.ID);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("The product has been deleted from the database!");
                else
                    Console.WriteLine("The product could not be deleted.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    #endregion

    #region Update Method from Database
    public static void UpdateProductToDatabase(Product product, string connectionString)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = "UPDATE products SET Name = @Name, Stock = @Stock WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ID", product.ID);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("The product has been updated in the database!");
                else
                    Console.WriteLine("The product could not be updated.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    #endregion

    #region Show Method from Database
    public static void ShowProductToDatabase(Product product, string connectionString)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM Products";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n--- Products ---");
                    while (reader.Read())
                    {
                        product.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : reader.GetInt32("ID");
                        product.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "(Boş)" : reader.GetString("Name");
                        product.Stock = reader.IsDBNull(reader.GetOrdinal("Stock")) ? 0 : reader.GetInt32("Stock");

                        Console.WriteLine($"ID: {product.ID}, Name: {product.Name}, Stock: {product.Stock}");
                    }
                    Console.WriteLine("----------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    #endregion
}