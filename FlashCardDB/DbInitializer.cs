using MySql.Data.MySqlClient;

public class DbInitializer
{

    
    private static readonly string connectionString_DataBase = "server=localhost;user id=root;password=;database=flashCardDB;";

    public static void CreateDataBase(string connectionString)
	{

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS flashCardDB";
                cmd.ExecuteNonQuery(); 
            }

            connection.Close();

        }

        using (var connection = new MySqlConnection(connectionString_DataBase))
        {
            connection.Open();
            
            using (var tableCmd = connection.CreateCommand()) 
            {

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS stacks (
                                            Stack_ID INT AUTO_INCREMENT PRIMARY KEY,
                                            Name VARCHAR(255) NOT NULL UNIQUE,
                                            Size INT DEFAULT 0
                                            );"; 

                tableCmd.ExecuteNonQuery();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS flashcards (
                                            Flashcards_ID INT AUTO_INCREMENT PRIMARY KEY,
                                            Front VARCHAR(255) NOT NULL,
                                            Back VARCHAR(255) NOT NULL,
                                            Stack_ID INT,
                                            FOREIGN KEY (Stack_ID) REFERENCES stacks(Stack_ID) ON DELETE CASCADE
                                            );";

                tableCmd.ExecuteNonQuery();



            }

        }

	}
}       
