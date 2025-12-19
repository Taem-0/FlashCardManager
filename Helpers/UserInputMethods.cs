
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


    }
}
