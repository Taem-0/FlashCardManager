using FlashCardManager.Controllers;
using FlashCardManager.DTO_s;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using FlashCardManager.SpectreConsole;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

                    while (true)
                    {
                        var stack =  StackService.SelectStackMenu();

                        if (stack == Stacks.EmptyStack) break;

                        while (true)
                        {
                            string userInput = DisplayStudySessionMenu(stack);


                            bool stayInStack = HandleStudySessionMenu(userInput, stack);
                            if (!stayInStack) break;
                        }

                        break;
                        
                        //I'm pretty sure I have lost my edge man, univ really fucked me over not giving me time to code my own shit
                        //I spent 2 hours in this, which is embarrasing man fuckk
                        //I used to just bust this out my ass in like 5 mins
                        //AND I'M SURE THIS IS SPAGHETTI CODE
                        //But hey it works

                    }
                    return true;
                default:
                    return true;
            }
        }

        private static string DisplayStudySessionMenu(Stacks stack)
        {

            DisplayMethods.TitleCard();

            AnsiConsole.MarkupLine($"Current working stack: {stack.name}\n");
            AnsiConsole.MarkupLine("--------------------------------------------------\n");


            List<string> menuChoices = [

                "Start",
                "Exit"

            ];

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(menuChoices)
            );

            return choice;
        }

        private static bool HandleStudySessionMenu(string userCommand, Stacks stack)
        {

            switch (userCommand)
            {
                case "Start":
                    StudySession(stack);
                    return true;
                case "Exit":
                    return false;
                default:
                    return true;
            }

        }



        private static void StudySession(Stacks stack)
        {
            int score = 0;


            int stackID = stack.id;

            var flashcardList = FlashcardController.ProcessGetFlashcardDTO(stackID);

            foreach (var item in flashcardList)
            {

                DisplayMethods.TitleCard();

                AnsiConsole.MarkupLine("Press enter to skip or 0 to exit");
                AnsiConsole.MarkupLine("\n--------------------------------------------------");

                TableVisualizer.flashCardFront(item);

                string userInput = UserInputMethods.PromptFlashcardQuestion(item);

                if (userInput == "0") break;

                if (userInput == "" || userInput == string.Empty)
                {

                    AnsiConsole.MarkupLine("Skipped.");
                    UserInputMethods.Pause();

                }
                else if (!userInput.Equals(item.backDTO))
                {

                    AnsiConsole.MarkupLine($"Wrong !! The correct answer is {item.backDTO}");
                    UserInputMethods.Pause();
                }
                else
                {
                    AnsiConsole.MarkupLine("Correct !!");
                    UserInputMethods.Pause();

                    score++;
                }


            }

            DisplayMethods.DisplayOverScreen(score, flashcardList);

        }




    }
}
