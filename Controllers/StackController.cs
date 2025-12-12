using System.Collections;
using FlashCardManager.DTO_s;
using FlashCardManager.FlashCardDB;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using FlashCardManager.SpectreConsole;

namespace FlashCardManager.Controllers
{
    internal class StackController
    {

        internal static void ProcessAdd(String stackName)
        {

            if (string.IsNullOrWhiteSpace(stackName) || stackName == "0")
            {
                Console.WriteLine("Cancelled.");
                Console.ReadLine();
                return;
            }

            Stacks stacks = new();

            stacks.name = stackName;

            DBmanager.PostStack(stacks);

        }

        //For actually doing something with the DB since we need the id of the selected stack. This will not be used to display whatsoever.
        internal static Stacks ProcessGetStack(String stackName)
        {

            Stacks stacks = null!;

            var Stack = DBmanager.GetStack();

            foreach (var item in Stack)
            {
                if (stackName.Equals(item.name))
                {
                    stacks = new Stacks();
                    stacks.id = item.id;
                    stacks.name = item.name;
                    stacks.size = item.size;
                    break;
                }
            }

            return stacks;

        }

        

        //For users interface basically, just takes the DTO
        internal static List<StackDTO> ProcessGetStackDTO()
        {
            List<StackDTO> tableDisplay = new();

            var Stack = DBmanager.GetStack();


            foreach (var item in Stack)
            {

                StackDTO stack = new StackDTO
                {
                    nameDTO = item.name?.ToString(),
                    sizeDTO = item.size?.ToString() ?? "N/A",
                };

                tableDisplay.Add(stack);

            }
            return tableDisplay;
        }



    }
}
