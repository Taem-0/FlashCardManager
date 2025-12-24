
using FlashCardManager.Client;
using FlashCardManager.Controllers;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using Spectre.Console;

namespace FlashCardManager.Services
{
    internal class FlashCardService
    {

        private readonly StackService _stackService = new();


        internal static void FlashCardStackSelectionMenu()
        {
            DisplayMethods.TitleCard();

            bool isInFlashCardStackSelection = true;

            while(isInFlashCardStackSelection)
            {
                isInFlashCardStackSelection = FlashCardMenu(StackService.SelectStackMenu());


            }
        }

        internal static bool FlashCardMenu(Stacks stacks)
        {

            DisplayMethods.TitleCard();

            

            while(true)
            {
                var refreshedStack = Methods.RefreshStack(stacks.id) ?? Stacks.EmptyStack;
                if (refreshedStack == Stacks.EmptyStack)
                {
                    
                    return false;
                }

                string userCommand = DisplayFlashCardSelectionAndInput();

                bool isInFlashCardMenu = HandleFlashCardMenuResponse(userCommand, refreshedStack);

                if (!isInFlashCardMenu)
                {
                    // Change stack OR return to main menu (Until now I dont know what to feel about this lmao)
                    // But the primeagan did say to get used to be okay if IT WORKS, even tho rn I'm refactoring again XD
                    return userCommand == "Change current stack";
                }
            }


        }

        private static string DisplayFlashCardSelectionAndInput()
        {
            DisplayMethods.TitleCard();

            List<string> menuChoices = [
                "Return to main menu",
                "Change current stack",
                "View all Flashcards in stack",
                "Create a Flashcard in current stack",
                "Edit a Flashcard",
                "Delete a Flashcard"
           ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
                );

            return choice;

        }

        private static bool HandleFlashCardMenuResponse(string userCommand, Stacks stacks)
        {

            switch( userCommand)
            {
                case "Return to main menu":
                    return false;
                case "Change current stack":
                    return false;
                case "View all Flashcards in stack":
                    DisplayFlashcardTable();
                    return true;
                case "Create a Flashcard in current stack":
                    CreateFlashCardMenu(stacks);
                    return true;
                case "Edit a Flashcard":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    UserInputMethods.Pause();
                    return true;
                case "Delete a Flashcard":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    UserInputMethods.Pause();
                    return true;
                default: 
                    return true;  

            }
        }


        internal static void DisplayFlashcardTable()
        {

            DisplayMethods.TitleCard();
            
            Methods.CheckFlashcards();
            AnsiConsole.MarkupLine("\n--------------------------------------------------");

            List<string> menuChoices = [
                "Return"
            ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
                );

            if (choice == "Return")
            {
                return;
            }

        }


        internal static void CreateFlashCardMenu(Stacks stacks)
        {

            DisplayMethods.TitleCard();

            string frontSide = UserInputMethods.PromptFlashCardFront();

            if (string.IsNullOrEmpty(frontSide))
            {

                AnsiConsole.MarkupLine("[red]Cancelled[/]");
                UserInputMethods.Pause();

                return;

            }


            string backSide = UserInputMethods.PromptFlashCardBack();

            if (string.IsNullOrEmpty(backSide))
            {

                AnsiConsole.MarkupLine("[red]Cancelled[/]");
                UserInputMethods.Pause();

                return;

            }

            HandleCreateFlashCard(frontSide, backSide, stacks);




        }

        private static void HandleCreateFlashCard(string frontSide, string backSide, Stacks stacks)
        {

            if (!FlashcardController.ProcessAdd(frontSide, backSide, stacks))
            {

                AnsiConsole.MarkupLine("[red]Card creation failed :<.[/]");
                UserInputMethods.Pause();

                return;

            }


            List<FlashCards> flashCards = FlashcardController.ProcessGetFlashcardByID(stacks.id);

            Stacks updatedStack = new()
            {
                id = stacks.id,
                name = stacks.name,
                size = flashCards.Count
            };

            

            if (!StackController.ProcessUpdate(updatedStack))
            {
                AnsiConsole.MarkupLine("[red]failed :<.[/]");
            }


            AnsiConsole.MarkupLine("[yellow]Successfully created card[/]");
            UserInputMethods.Pause();


        }




    }
}
