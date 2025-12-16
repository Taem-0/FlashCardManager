
using FlashCardManager.Controllers;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using FlashCardManager.SpectreConsole;

namespace FlashCardManager.Client
{
    internal class UserInterface
    {

        internal void MainMenu()
        {
            bool closeApp = false;

            while(!closeApp)
            {

                DisplayMenu();

                String? userCommand = Console.ReadLine()?.Trim() ?? "";

                closeApp = HandleMainMenuResponse(userCommand);
            }
        }


        private void DisplayMenu()
        {

            DisplayMethods.TitleCard();

            Console.WriteLine("0: exit");
            Console.WriteLine("1: manage stacks");
            Console.WriteLine("2: manage flashcards");
            Console.WriteLine("3: study");
            Console.WriteLine("4: view study session data");

        }


        private bool HandleMainMenuResponse(string userCommand)
        {

            switch (userCommand)
            {
                case "0":
                    return true;
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

            return false;

        }






        internal void StackSelectionMenu()
        {

            DisplayMethods.TitleCard();

            bool isInStackSelection = true;

            while (isInStackSelection)
            {

                DisplayStackSelection();

                String? userCommand = Console.ReadLine()?.Trim() ?? "";

                isInStackSelection = HandleStackSelectionResponse(userCommand);

                if (!isInStackSelection) break;

                var stacks = StackController.ProcessGetStack(userCommand);

                if (stacks == null)
                {

                    DisplayMethods.TitleCard();
                    Console.WriteLine("Invalid.");
                    Console.ReadLine();
                    continue;
                    
                }

                SelectedStackMenu(stacks);

            }
        }


        private void DisplayStackSelection()
        {

            DisplayMethods.SpecificClear(19, 10);

            Methods.CheckStacks();

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("0: exit");
            Console.WriteLine("1: create new stack");

        }


        private bool HandleStackSelectionResponse(string userCommand)
        {

            switch (userCommand)
            {
                case "0":
                    return false;
                case "1":
                    CreateStackMenu();
                    break;

            }

            return true;

        }






        internal void SelectedStackMenu(Stacks stacks)
        {
            DisplayMethods.TitleCard();

            bool isInSelectedStack = true;

            while (isInSelectedStack)
            {
                
                DisplaySelectedStack(stacks);

                String? userCommand = Console.ReadLine()?.Trim()!;

                isInSelectedStack = HandleSelectedStackResponse(userCommand, stacks);

                if (!isInSelectedStack) break;
                
            }
        }


        private void DisplaySelectedStack(Stacks stacks)
        {
            Console.WriteLine($"Current working stack: {stacks.name} ");
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("0: return to main menu");
            Console.WriteLine("D: delete current stack");
        }

        private bool HandleSelectedStackResponse(string userCommand, Stacks stacks)
        {

            switch (userCommand)
            {
                case "0":
                    return false;
                case "D":
                    ConfirmDelete(stacks);
                    return false;
                    
            }

            return true;

        }






        internal void CreateStackMenu()
        {

            DisplayMethods.TitleCard();


            string userCommand = UserInputMethods.PromptUserStackName();

            if (string.IsNullOrEmpty(userCommand))
            {
                Console.WriteLine("Cancelled.");
                Console.ReadLine();
                return;
            }


            HandleCreateStackResponse(userCommand);
            
        }


        private void HandleCreateStackResponse(string stackName)
        {

            if (!StackController.ProcessAdd(stackName))
            {
                return;
            }

            Console.Write("Successfully created stack.");
            Console.ReadLine();

        }






        internal void ConfirmDelete(Stacks currentStack)
        {

            DisplayMethods.TitleCard();

            string userCommand = UserInputMethods.PromptUserConfirmation();

            HandleUserDeleteResponse(userCommand, currentStack); 

        }


        internal void HandleUserDeleteResponse(string userCommand, Stacks currentStack)
        {

            if (userCommand.Equals("y"))
            {
                StackController.ProcessDeleteStack(currentStack);
                Console.WriteLine("Stack deleted successfully");
                Console.ReadLine();
            }
            else if (userCommand.Equals("n"))
            {
                Console.WriteLine("Cancelled successfully");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid");
                Console.ReadLine();
            }

        }



    }
}
