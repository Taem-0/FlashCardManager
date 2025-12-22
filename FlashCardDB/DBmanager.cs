
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using MySql.Data.MySqlClient;

namespace FlashCardManager.FlashCardDB
{
    internal class DBmanager
    {

        private static readonly string connectionString_DataBase = "server=localhost;user id=root;password=;database=flashCardDB;";

        #region StacksDBmanager
        internal static void PostStack(Stacks stacks)
        {

            using var connection = Methods.CreateConnection(connectionString_DataBase);
            using var command = connection.CreateCommand();
            try
            {

                command.CommandText = @"INSERT INTO stacks (Name)
                                                VALUES(@name)";

                command.Parameters.AddWithValue("@name", stacks.name);


                int rowsAffected = command.ExecuteNonQuery();


            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        internal static void UpdateStack(Stacks stacks)
        {

            using var connection = Methods.CreateConnection(connectionString_DataBase);
            using var command = connection.CreateCommand();
            try
            {

                command.CommandText = @"UPDATE stacks
                                                SET Name = @name, Size = @size
                                                WHERE Stack_ID = @id";

                command.Parameters.AddWithValue("@name", stacks.name);
                command.Parameters.AddWithValue("@size", stacks.size);
                command.Parameters.AddWithValue("@id", stacks.id);

                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }



        } 


        internal static List<Stacks> GetStack()
        {
            List<Stacks> tableData = [];

            using (var connection = Methods.CreateConnection(connectionString_DataBase))
            using(var command = connection.CreateCommand())
            {
                try
                {

                    command.CommandText = @"SELECT * FROM stacks;";

                    using var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                        return tableData;

                    ReadStacks(reader, tableData);

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                
            }

            return tableData;

        }

        
        private static void ReadStacks(MySqlDataReader reader, List<Stacks> tableData)
        {

            while (reader.Read())
            {
                tableData.Add(new Stacks
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    size = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                });
            }

        }



        internal static void DeleteStack(int id)
        {

            using var connection = Methods.CreateConnection(connectionString_DataBase);
            using var command = connection.CreateCommand();

            try
            {

                command.CommandText = @"DELETE FROM stacks WHERE Stack_ID = @id";

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion


        #region FlashCardsDBmanager

        internal static void PostFlashCard(FlashCards flashCards)
        {

            using var connection = Methods.CreateConnection(connectionString_DataBase);
            using var command = connection.CreateCommand();
            try
            {
                command.CommandText = @"INSERT INTO flashcards (Front, Back, Stack_ID)
                                            VALUES(@front, @back, @stackID)";

                command.Parameters.AddWithValue("@front", flashCards.front);
                command.Parameters.AddWithValue("@back", flashCards.back);
                command.Parameters.AddWithValue("stackID", flashCards.stackId);

                int rowsAffected = command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        #endregion





    }
}
