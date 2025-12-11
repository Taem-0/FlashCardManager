using FlashCardManager.Client;

namespace FlashCardManager
{
    internal class Program
    {

        private static readonly string connectionString = "server=localhost;user id=root;password=;";

        static void Main(string[] args)
        {
            //Initializes my database
            DbInitializer dbManager = new DbInitializer();
            DbInitializer.CreateDataBase(connectionString);

            //Initializes my application
            UserInterface userInterface = new UserInterface();
            userInterface.MainMenu();

        }
    }
}
