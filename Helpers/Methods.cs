
using FlashCardManager.Controllers;
using FlashCardManager.SpectreConsole;
using MySql.Data.MySqlClient;

namespace FlashCardManager.Helpers
{
    internal class Methods
    {

        internal static void CheckStacks()
        {
            var stackList = StackController.ProcessGetStackDTO();

            if (stackList.Count > 0)
            {
                TableVisualizer.stackTable(stackList);
            }
            else
            {
                Console.WriteLine("No records found :<");
            }
        }


        internal static MySqlConnection CreateConnection(string connectionString)
        {

            var connection = new MySqlConnection(connectionString);

            connection.Open();

            return connection;

        }

    }
}
