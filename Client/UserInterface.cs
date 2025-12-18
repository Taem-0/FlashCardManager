
using FlashCardManager.Controllers;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using Spectre.Console;

namespace FlashCardManager.Client
{
    internal class UserInterface
    {

        internal void MainMenu()
        {
            bool closeApp = false;

            while(!closeApp)
            {

                String userCommand = DisplayMenuAndInput();

                closeApp = HandleMainMenuResponse(userCommand);

            }

            
        }


        private string DisplayMenuAndInput()
        {

            DisplayMethods.TitleCard();

            List<string> menuChoices = [
                "Exit",
                "Manage Stacks",
                "Manage Flashcards",
                "Study",
                "View study sessions"
            ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
                );

            return choice;

        }

        

        private bool HandleMainMenuResponse(string userCommand)
        {

            switch (userCommand)
            {
                case "Exit":
                    return true;
                case "Manage Stacks":
                    StackSelectionMenu();
                    break;
                case "Manage Flashcards":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    Console.ReadLine();
                    break;
                case "Study":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    Console.ReadLine();
                    break;
                case "View study sessions":
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

                String? userCommand = DisplayStackSelectionAndInput();

                if (string.IsNullOrEmpty(userCommand))
                    continue;

                isInStackSelection = HandleStackSelectionResponse(userCommand);

                if (!isInStackSelection) break;


                

            }
        }


        private string DisplayStackSelectionAndInput()
        {

            DisplayMethods.SpecificClear(19, 10);

            Methods.CheckStacks();
            AnsiConsole.MarkupLine("\n--------------------------------------------------");


            List<string> menuChoices = [
                "Return",
                "Create new stack",
                "Select a stack"
            ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
                );

            return choice;

        }



        private bool HandleStackSelectionResponse(string userCommand)
        {

            switch (userCommand)
            {
                case "Return":
                    return false;
                case "Create new stack":
                    CreateStackMenu();
                    return true;
                case "Select a stack":
                    SelectStackMenu();
                    return true;

            }

            return true;

        }




        internal void SelectStackMenu()
        {

            bool isInSelectionStack = true;

            while (isInSelectionStack)
            {

                DisplayMethods.TitleCard();

                var stacks = StackController.ProcessGetStack();


                if (stacks.Count.Equals(0))
                {

                AnsiConsole.MarkupLine("[red]Invalid.[/]");
                AnsiConsole.Prompt(
                    new TextPrompt<string>("Press enter to continue").AllowEmpty()
                );
                return;

                }


                var selectedStack = AnsiConsole.Prompt(
                new SelectionPrompt<Stacks>()
                    .Title("Select a stack:")
                    .PageSize(5)
                    .AddChoices(stacks)
                    .MoreChoicesText("Move down to reveal more")
                    .UseConverter(stack => stack.name!)
                );

                bool continueSelection = SelectedStackMenu(selectedStack);
                if (!continueSelection)
                {
                    isInSelectionStack = false;
                }

            }
        }




        internal void CreateStackMenu()
        {

            DisplayMethods.TitleCard();


            string userCommand = UserInputMethods.PromptUserStackName();

            if (string.IsNullOrEmpty(userCommand))
            {
                AnsiConsole.MarkupLine("Cancelled");
                AnsiConsole.Prompt(
                    new TextPrompt<string>("Press enter to continue")
                    .AllowEmpty()
                );

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

            AnsiConsole.MarkupLine("Successfully created stack");
            AnsiConsole.Prompt(new TextPrompt<string>("Press enter to continue"));

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






        internal bool SelectedStackMenu(Stacks stacks)
        {
            DisplayMethods.TitleCard();

            
            bool isInSelectedStack = true;
            string lastCommand = "";
            while (isInSelectedStack)
            {

                var refreshedStack = Methods.RefreshStack(stacks.id);
                if (refreshedStack == null)
                {
                    Console.WriteLine("Error: Stack no longer exists.");
                    Console.ReadLine();
                    return true;
                }


                String? userCommand = DisplaySelectedStack(refreshedStack);
                lastCommand = userCommand;

                isInSelectedStack = HandleSelectedStackResponse(userCommand, refreshedStack);

                if (!isInSelectedStack) break;

            }

            return lastCommand == "x";
        }


        private string DisplaySelectedStack(Stacks stacks)
        {
            DisplayMethods.TitleCard();

            List<string> menuChoices = [
                "Return to main menu",
                "Change current stack",
                "Edit current stack",
                "Delete current stack"
            ];

            AnsiConsole.MarkupLine($"Current working stack: {stacks.name} ");
            AnsiConsole.MarkupLine("\n--------------------------------------------------");
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
                );

            return choice;

        }



        private bool HandleSelectedStackResponse(string userCommand, Stacks stacks)
        {

            switch (userCommand)
            {
                case "Return to main menu":
                    return false;
                case "Change current stack":
                    return true;
                case "Edit current stack":
                    ConfirmUpdateStack(stacks);
                    return true;
                case "Delete current stack":
                    ConfirmDelete(stacks);
                    return false;

            }

            return true;

        }
    }
}
