
using Spectre.Console;

namespace FlashCardManager.Helpers
{
    internal class UserInputMethods
    {

        internal static string PromptUserConfirmation()
        {

            String userCommand = AnsiConsole.Prompt(
                new TextPrompt<string>("Are you sure?")
                .AddChoice("y")
                .AddChoice("n")
                );

            return userCommand;

        }


        internal static string PromptUserStackName()
        {
            var input = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter stack name or [grey]0 to cancel[/]:")
                    .AllowEmpty()
            ).Trim();

            return input == "0" ? string.Empty : input;
        }


        internal static void Pause()
        {
            AnsiConsole.Prompt(
                new TextPrompt<string>("Press enter to continue")
                    .AllowEmpty()
            );
        }

        internal static string PromptFlashCardName()
        {

            string input = AnsiConsole.Prompt(
                new TextPrompt<String>("Enter the side or [grey]0 to cancel[/]:")
                    .AllowEmpty()
            ).Trim();

            if (string.IsNullOrWhiteSpace(input) || input.Equals("0"))
            {

                return string.Empty;
                

            }

            return input;

        }




        internal static int PromptFlashCardID()
        {

            string input = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the id of the flashcard or [grey]0 to cancel[/]:  ")
                    .AllowEmpty()
            );

           

            if (string.IsNullOrWhiteSpace(input))
            {

                AnsiConsole.MarkupLine("[red]Invalid[/]");
                UserInputMethods.Pause();
                return 0;

            }

            _ = int.TryParse(input, out int id);

            return id;


        }
    }
}
