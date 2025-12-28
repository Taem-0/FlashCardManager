
using FlashCardManager.Client;
using FlashCardManager.Controllers;
using FlashCardManager.DTO_s;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using Spectre.Console;
using Spectre.Console.Cli;

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
                    DisplayFlashcardTable(stacks);
                    return true;
                case "Create a Flashcard in current stack":
                    CreateFlashCardMenu(stacks);
                    return true;
                case "Edit a Flashcard":
                    DisplayEditMenu(stacks);
                    return true;
                case "Delete a Flashcard":
                    DisplayDeleteMenu(stacks);
                    return true;
                default: 
                    return true;  

            }
        }




        internal static void DisplayFlashcardTable(Stacks stacks)
        {

            DisplayMethods.TitleCard();
            
            Methods.CheckFlashcards(stacks);
            AnsiConsole.MarkupLine("\n------------------------------\n");


            UserInputMethods.Pause();

        }


        internal static void CreateFlashCardMenu(Stacks stacks)
        {

            DisplayMethods.TitleCard();

            string frontSide = UserInputMethods.PromptFlashCardName();

            if (string.IsNullOrEmpty(frontSide))
            {

                AnsiConsole.MarkupLine("[red]Cancelled[/]");
                UserInputMethods.Pause();

                return;

            }


            string backSide = UserInputMethods.PromptFlashCardName();

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




        private static void DisplayEditMenu(Stacks stacks)
        {

            bool isInEdit = true;

            while (isInEdit)
            {

                DisplayMethods.TitleCard();

                List<string> menuChoices = [
                    "Return",
                    "Select a flashcard"
                ];

                string choice = AnsiConsole.Prompt(
                new SelectionPrompt<String>()
                    .AddChoices(menuChoices)
                );

                isInEdit = HandleEditMenu(stacks, choice);

            }

        }


        internal static bool HandleEditMenu(Stacks stacks, String choice)
        {

            DisplayMethods.TitleCard();

            if (choice.Equals("Return"))
            {

                return false;

            }

            var selectedFlashcard = SelectFlashCard(stacks);

            if (selectedFlashcard.Equals(FlashCards.EmptyFlashCard))
            {
                return true;
            }

            PromptFlashcardSide(selectedFlashcard);

            return true;

        }

        internal static void PromptFlashcardSide(FlashCards flashCard)
        {

            DisplayMethods.TitleCard();

            List<string> menuChoices = [
                   "Return",
                   "Front",
                   "Back"
               ];

            string choice = AnsiConsole.Prompt(
            new SelectionPrompt<String>()
                .AddChoices(menuChoices)
            );

            HandleFlashCardUpdate(flashCard, choice);

        } 


        internal static void HandleFlashCardUpdate(FlashCards flashCard, string choice)
        {

            string userInput;


            switch (choice)
            {
                case "Return":
                    return;
                case "Front":

                    userInput = UserInputMethods.PromptFlashCardName();

                    if (string.IsNullOrEmpty(userInput))
                    {

                        AnsiConsole.MarkupLine("[red]Cancelled[/]");
                        UserInputMethods.Pause();

                        return;

                    }

                    if (!FlashcardController.ProcessUpdateFront(flashCard, userInput))
                    {
                        AnsiConsole.MarkupLine("[red]Failed to update :<[/]");
                    }

                    AnsiConsole.MarkupLine("[green]Successfully updated[/]");
                    UserInputMethods.Pause();

                    break;

                case "Back":

                    userInput = UserInputMethods.PromptFlashCardName();

                    if (string.IsNullOrEmpty(userInput))
                    {

                        AnsiConsole.MarkupLine("[red]Cancelled[/]");
                        UserInputMethods.Pause();

                        return;

                    }

                    AnsiConsole.MarkupLine("[green]Successfully updated[/]");
                    UserInputMethods.Pause();

                    if (!FlashcardController.ProcessUpdateBack(flashCard, userInput))
                    {
                        AnsiConsole.MarkupLine("[red]Failed to update :<[/]");
                    }

                    break;
            }

        }

        


        private static void DisplayDeleteMenu(Stacks stacks)
        {

            bool isDeleting = true;

            while (isDeleting)
            {

                DisplayMethods.TitleCard();

                List<string> menuChoices = [
                    "Return",
                    "Select a flashcard"
                ];

                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<String>()
                    .PageSize(5)
                    .AddChoices(menuChoices)
                );

                isDeleting = HandleDeleteMenu(stacks, choice);

            }

        }


        internal static bool HandleDeleteMenu(Stacks stacks, string menuChoice)
        {

            DisplayMethods.TitleCard();

            if (menuChoice.Equals("Return"))
            {

                return false;

            }

            var selectedFlashcard = SelectFlashCard(stacks);

            if (selectedFlashcard.Equals(FlashCards.EmptyFlashCard))
            {
                return true;
            }

            ConfirmDelete(selectedFlashcard);

            return true;

        }


        internal static void ConfirmDelete(FlashCards selectedFlashcard)
        {

            DisplayMethods.TitleCard();

            string userCommand = UserInputMethods.PromptUserConfirmation();

            HandleDeleteResponse(userCommand, selectedFlashcard);

        }


        internal static void HandleDeleteResponse(string userCommand, FlashCards selectedFlashcard)
        {

            if (userCommand.Equals("y"))
            {
                FlashcardController.ProcessDeleteFlashcard(selectedFlashcard);
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




        internal static FlashCards SelectFlashCard(Stacks stacks)
        {

            List<FlashCards> flashcards = FlashcardController.ProcessGetFlashcardByID(stacks.id);


            if (flashcards.Count.Equals(0))
            {

                AnsiConsole.MarkupLine("[red]No stacks found :<.[/]");
                UserInputMethods.Pause();
                return FlashCards.EmptyFlashCard;

            }


            var selectedFlashcard = AnsiConsole.Prompt(
            new SelectionPrompt<FlashCards>()
                .Title("Select a flashcard:")
                .PageSize(5)
                .AddChoices(flashcards)
                .MoreChoicesText("Move down to reveal more")
                .UseConverter(flashcards => flashcards.front ?? "[Empty]")
            );

            return selectedFlashcard;

        }


    }
}
