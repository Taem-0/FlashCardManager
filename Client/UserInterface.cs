
using System.Diagnostics;
using FlashCardManager.Controllers;
using FlashCardManager.Helpers;
using FlashCardManager.Models;

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
                case "x":
                    StackSelectionMenu();
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

            Console.WriteLine("--------------------------------------------------\n");
            Console.WriteLine("Input a current stack name or input the options below");
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






        internal void ConfirmUpdateStack(Stacks currentStack)
        {
            DisplayMethods.TitleCard();

            string userCommand = UserInputMethods.PromptUserStackName();

            HandleUpdateStackResponse(userCommand, currentStack);

        }


        private void HandleUpdateStackResponse(string userCommand, Stacks currentStack)
        {

            if (string.IsNullOrEmpty(userCommand) || userCommand == "0")
            {
                Console.WriteLine("Cancelled.");
                Console.ReadLine();
                return;
            }

            if (!StackController.ProcessUpdate(userCommand, currentStack))
            {
                Console.WriteLine("Cancelled");
                return;
            }


            Console.Write("Successfully updated stack.");
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






        internal void SelectedStackMenu(Stacks stacks)
        {
            DisplayMethods.TitleCard();

            
            bool isInSelectedStack = true;

            while (isInSelectedStack)
            {

                var refreshedStack = Methods.RefreshStack(stacks.id);
                if (refreshedStack == null)
                {
                    Console.WriteLine("Error: Stack no longer exists.");
                    Console.ReadLine();
                    return;
                }

                DisplaySelectedStack(refreshedStack);

                String? userCommand = Console.ReadLine()?.Trim().ToLower()!;

                isInSelectedStack = HandleSelectedStackResponse(userCommand, refreshedStack);

                if (!isInSelectedStack) break;

            }
        }


        private void DisplaySelectedStack(Stacks stacks)
        {
            DisplayMethods.TitleCard();

            Console.WriteLine($"Current working stack: {stacks.name} ");
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("0: return to main menu");
            Console.WriteLine("X: change current stack");
            Console.WriteLine("E: edit current stack");
            Console.WriteLine("D: delete current stack");

        }


        private bool HandleSelectedStackResponse(string userCommand, Stacks stacks)
        {

            switch (userCommand)
            {
                case "0":
                    return false;
                case "x":
                    return false;
                case "e":
                    ConfirmUpdateStack(stacks);
                    return true;
                case "d":
                    ConfirmDelete(stacks);
                    return false;

            }

            return true;

        }


    }
}
