

using FlashCardManager.Controllers;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using Spectre.Console;

namespace FlashCardManager.Client
{
    internal class StackService
    {

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
                    UserInputMethods.Pause();
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
                UserInputMethods.Pause();

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
            UserInputMethods.Pause();

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
                UserInputMethods.Pause();
                return;
            }

            if (!StackController.ProcessUpdate(userCommand, currentStack))
            {
                Console.WriteLine("Cancelled");
                UserInputMethods.Pause();
                return;
            }


            Console.Write("Successfully updated stack.");
            UserInputMethods.Pause();


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
                UserInputMethods.Pause();
            }
            else if (userCommand.Equals("n"))
            {
                Console.WriteLine("Cancelled successfully");
                UserInputMethods.Pause();
            }
            else
            {
                Console.WriteLine("Invalid");
                UserInputMethods.Pause();
            }

        }






        internal bool SelectedStackMenu(Stacks stacks)
        {
            DisplayMethods.TitleCard();

            while (true)
            {
                var refreshedStack = Methods.RefreshStack(stacks.id);
                if (refreshedStack == null)
                {
                    Console.WriteLine("Error: Stack no longer exists.");
                    UserInputMethods.Pause();
                    return true;
                }

                string userCommand = DisplaySelectedStack(refreshedStack);

                bool stayInMenu = HandleSelectedStackResponse(userCommand, refreshedStack);

                if (!stayInMenu)
                {
                    // Change stack OR return to main menu
                    return userCommand == "Change current stack";
                }
            }
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
                    return false;
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
