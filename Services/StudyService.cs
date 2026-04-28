using FlashCardManager.Helpers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCardManager.Services
{
    internal class StudyService
    {

        internal static void StudySelection()
        {

            DisplayMethods.TitleCard();

            bool isInStudySelection = true;

            while (isInStudySelection)
            {

                string userCommand = DisplayStudySelectionInput();

                isInStudySelection = HandleStudySelectionInput(userCommand);

            }

        }


        private static string DisplayStudySelectionInput()
        {

            DisplayMethods.TitleCard();

            List<string> menuChoices = [

                "Return to main menu",
                "Select stack"

            ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
            );

            return choice;
        }



        private static bool HandleStudySelectionInput(string userCommand)
        {

            switch(userCommand)
            {
                case "Return to main menu":
                    return false;
                case "Select stack":
                    AnsiConsole.MarkupLine("CALLED A METHOD, IN CONSTRUCTION !!");
                    UserInputMethods.Pause();
                    return false;
                default:
                    return true;

            }


        }
    }
}
