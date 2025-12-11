
using FlashCardManager.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace FlashCardManager.FlashCardDB
{
    internal class DBmanager
    {

        private static readonly string connectionString_DataBase = "server=localhost;user id=root;password=;database=flashCardDB;";

        #region StacksDBmanager
        internal static void PostStack(Stacks stacks)
        {

            using (var connection = new MySqlConnection(connectionString_DataBase))
            {

                connection.Open();

                using (var command = connection.CreateCommand())
                {

                    command.CommandText = @"INSERT INTO stacks (Name)
                                                VALUES(@name)";

                    command.Parameters.AddWithValue("@name", stacks.name);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Successfully created stack.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("No rows affected.");
                            Console.ReadLine();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    
                }

            }

        }

        internal static List<Stacks> GetStack()
        {

            using (var connection = new MySqlConnection(connectionString_DataBase))
            {
             
                List<Stacks> tableData = new();
                
                using(var command = connection.CreateCommand())
                {

                    connection.Open();

                    command.CommandText = @"SELECT * FROM stacks;";

                    using(var reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                tableData.Add(new Stacks
                                {
                                    id = reader.GetInt32(0),
                                    name = reader.GetString(1),
                                    size = reader.GetInt32(2)
                                });
                            }

                        }

                        return tableData;
                    }
                }
            }
        }







        #endregion
    }
}
