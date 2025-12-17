
namespace FlashCardManager.Helpers
{
    internal class UserInputMethods
    {

        internal static string PromptUserConfirmation()
        {

            Console.WriteLine("Are you sure you? (y/n)");

            String userCommand = Console.ReadLine()?.Trim().ToLower() ?? "";

            return userCommand;

        }

        internal static string PromptUserStackName()
        {
            Console.WriteLine("Enter stack name or 0 to cancel: ");
            String stackName = Console.ReadLine()!.Trim();

            
            return stackName;

        }



    }
}
