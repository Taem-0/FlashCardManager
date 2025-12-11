
using FlashCardManager.Helpers;
using FlashCardManager.Controllers;

namespace FlashCardManager.Client
{
    internal class UserInterface
    {

        StackController stackController = new();

        internal void MainMenu()
        {
            bool closeApp = false;

            while(closeApp == false)
            {

                Methods.TitleCard();

                Console.WriteLine("0: exit");
                Console.WriteLine("1: manage stacks");
                Console.WriteLine("2: manage flashcards");
                Console.WriteLine("3: study");
                Console.WriteLine("4: view study session data");

                String? userCommand = Console.ReadLine();

                switch(userCommand)
                {
                    case "0":
                        closeApp = true;
                        break;
                    case "1":
                        StackSelection();
                        break;
                    case "2":
                        Console.WriteLine("UNDER CONSTRUCTION");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("UNDER CONSTRUCTION");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.WriteLine("UNDER CONSTRUCTION");
                        Console.ReadLine();
                        break;
                }
            }
        }

        internal void StackSelection()
        {

            Methods.TitleCard();

            bool stackSelection = true;

            while (stackSelection == true)
            {
                Methods.SpecificClear(19, 10);

                StackController.ProcessGet();

                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("0: exit");
                Console.WriteLine("1: create new stack");

                String? userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case "0":
                        stackSelection = false;
                        break;
                    case "1":
                        StackController.ProcessAdd();
                        break;
                    default:
                        Methods.TitleCard();
                        Console.WriteLine("Invalid.");
                        Console.ReadLine();
                        break;
                }

            }
        }

        

        

    }
}
