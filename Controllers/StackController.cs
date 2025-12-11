using FlashCardManager.DTO_s;
using FlashCardManager.FlashCardDB;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using FlashCardManager.SpectreConsole;

namespace FlashCardManager.Controllers
{
    internal class StackController
    {

        internal static void ProcessAdd()
        {
            Methods.TitleCard();

            Console.WriteLine("Enter stack name or 0 to cancel: ");

            String stackName = Console.ReadLine();

            if (stackName != "0")
            {
                Stacks stacks = new();

                stacks.name = stackName;
                    
                DBmanager.PostStack(stacks);

                return;
            }


            Console.WriteLine("Cancelled.");
            Console.ReadLine();
            

        }

        internal static void ProcessGet()
        {
            List<StackDTO> tableDisplay = new();

            var Stack = DBmanager.GetStack();

            if(Stack.Count > 0)
            {
                foreach (var item in Stack)
                {

                    StackDTO stack = new StackDTO
                    {
                        nameDTO = item.name.ToString(),
                        sizeDTO = Stack.Count > 1 ? item.size.ToString() : "N/A",
                    };

                    tableDisplay.Add(stack);

                }

                TableVisualizer.stackTable(tableDisplay);

            }
            else
            {
                Console.WriteLine("No records found... :'(");
            }

        }

    }
}
