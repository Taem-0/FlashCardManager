

using FlashCardManager.Helpers;
using FlashCardManager.Models;
using Spectre.Console;

namespace FlashCardManager.Services
{
    internal class FlashCardService
    {

        private readonly StackService _stackService = new();


        internal void FlashCardStackSelectionMenu()
        {
            DisplayMethods.TitleCard();

            bool isInFlashCardStackSelection = true;

            while(isInFlashCardStackSelection)
            {
                isInFlashCardStackSelection = FlashCardMenu(_stackService.SelectStackMenu());
            }
        }

        internal bool FlashCardMenu(Stacks stacks)
        {

            DisplayMethods.TitleCard();

            

            while(true)
            {
                var refreshedStack = Methods.RefreshStack(stacks.id) ?? Stacks.EmptyStack;
                if (refreshedStack == Stacks.EmptyStack)
                {
                    Console.WriteLine("Error: Stack no longer exists.");
                    UserInputMethods.Pause();
                    return true;
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

        private string DisplayFlashCardSelectionAndInput()
        {
            DisplayMethods.TitleCard();

            List<string> menuChoices = [
                "Return to main menu",
                "Change current stack",
                "View all Flashcards in stack",
                "Create a Flashcard in current stack",
                "Edit current Flashcard",
                "Delete a Flashcard"
           ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
                );

            return choice;

        }

        private bool HandleFlashCardMenuResponse(string userCommand, Stacks stacks)
        {

            switch( userCommand)
            {
                case "Return to main menu":
                    return false;
                case "Change current stack":
                    return false;
                case "View all Flashcards in stack":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    UserInputMethods.Pause();
                    return true;
                case "Create a Flashcard in current stack":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    UserInputMethods.Pause();
                    return true;
                case "Edit current Flashcard":
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


    }
}
