
using FlashCardManager.Controllers;
using FlashCardManager.DTO_s;
using FlashCardManager.Models;
using FlashCardManager.SpectreConsole;
using MySql.Data.MySqlClient;

namespace FlashCardManager.Helpers
{
    internal class Methods
    {

        internal static List<StackDTO> CheckStacks()
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

            return stackList;
        }


        internal static List<FlashcardDTO> CheckFlashcards(Stacks stacks)
        {

            int stackID = stacks.id;

            var flashcardList = FlashcardController.ProcessGetFlashcardDTO(stackID);

            if (flashcardList.Count > 0)
            {
                TableVisualizer.flashCardTable(flashcardList);
            }
            else
            {
                Console.WriteLine("No records found :<");
            }

            return flashcardList;
        }
        


        internal static MySqlConnection CreateConnection(string connectionString)
        {

            var connection = new MySqlConnection(connectionString);

            connection.Open();

            return connection;

        }

        internal static Stacks? RefreshStack(int id)
        {


            return StackController.ProcessGetStackByID(id);


        }

    }
}
