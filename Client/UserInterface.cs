
using FlashCardManager.Controllers;
using FlashCardManager.DTO_s;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using FlashCardManager.SpectreConsole;

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

                String? userCommand = Console.ReadLine()?.Trim();

                switch(userCommand)
                {
                    case "0":
                        closeApp = true;
                        break;
                    case "1":
                        StackSelectionMenu();
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
                    default:
                        Console.WriteLine("Invalid.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        internal void StackSelectionMenu()
        {

            Methods.TitleCard();

            bool isInStackSelection = true;

            while (isInStackSelection == true)
            {
                Methods.SpecificClear(19, 10);

                var stackList = StackController.ProcessGetStackDTO();

                if (stackList.Count > 0)
                {
                    TableVisualizer.stackTable(stackList);
                }
                else
                {
                    Console.WriteLine("No records found :<");
                }

                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("0: exit");
                Console.WriteLine("1: create new stack");

                String? userCommand = Console.ReadLine()?.Trim();

                switch (userCommand)
                {
                    case "0":
                        isInStackSelection = false;
                        break;
                    case "1":
                        CreateStackMenu();
                        break;
                    default:
                        
                        var stacks = VerifyStackInput(userCommand!, stackList);

                        if (stacks != null)
                        {
                            SelectedStackMenu(stacks);
                        }

                        break;
                }



            }
        }


        internal void CreateStackMenu()
        {
            Methods.TitleCard();

            Console.WriteLine("Enter stack name or 0 to cancel: ");

            String stackName = Console.ReadLine()!.Trim();

            StackController.ProcessAdd(stackName);
        }


        internal void SelectedStackMenu(Stacks stacks)
        {
            Methods.TitleCard();

            bool isInSelectedStack = true;

            while (isInSelectedStack == true)
            {
                Console.WriteLine($"Current working stack: {stacks.name} ");
                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("0: return to main menu");

                ConsoleKeyInfo userInput = Console.ReadKey();

                switch (userInput.Key)
                {
                    case ConsoleKey.D0:
                        isInSelectedStack = false;
                        break;
                }
            }

            

        }
        

        internal Stacks VerifyStackInput(string userInput, List<StackDTO> stackList)
        {

            foreach (var stack in stackList)
            {
                if (string.Equals(userInput, stack.nameDTO))
                {

                    return StackController.ProcessGetStack(stack.nameDTO!);


                }
            }


            Console.WriteLine("Invalid.");
            Console.ReadLine();
            return null!;

        }
        

    }
}
