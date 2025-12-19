
using System.Runtime.CompilerServices;
using FlashCardManager.Helpers;
using FlashCardManager.Services;
using Spectre.Console;

namespace FlashCardManager.Client
{
    internal class UserInterface
    {

        private readonly StackService _stackService = new();
        private readonly FlashCardService _flashCardService = new();   


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
                    _stackService.StackSelectionMenu();
                    break;
                case "Manage Flashcards":
                    _flashCardService.FlashCardStackSelectionMenu();
                    break;
                case "Study":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    UserInputMethods.Pause();
                    break;
                case "View study sessions":
                    Console.WriteLine("UNDER CONSTRUCTION");
                    UserInputMethods.Pause();
                    break;
                case "x":
                    _stackService.StackSelectionMenu();
                    break;
                default:
                    Console.WriteLine("Invalid.");
                    UserInputMethods.Pause();
                    break;
            }

            return false;

        }
        
    }
}
