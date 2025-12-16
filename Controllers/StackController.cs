using System.Collections;
using FlashCardManager.DTO_s;
using FlashCardManager.FlashCardDB;

using FlashCardManager.Models;

namespace FlashCardManager.Controllers
{
    internal class StackController
    {

        internal static bool ProcessAdd(String stackName)
        {

            if (string.IsNullOrWhiteSpace(stackName) || stackName == "0")
            {
                
                return false;
            }

            Stacks stacks = new();

            stacks.name = stackName;

            DBmanager.PostStack(stacks);

            return true;

        }


        internal static void ProcessDeleteStack(Stacks currentStack)
        {

            int stackID = currentStack.id;
            DBmanager.DeleteStack(stackID);

        }

        //For actually doing something with the DB since we need the id of the selected stack. This will not be used to display whatsoever.
        internal static Stacks? ProcessGetStack(String stackName)
        {

            var Stack = DBmanager.GetStack();

            foreach (var item in Stack)
            {
                if (!stackName.Equals(item.name))
                {
                    continue;  
                }

                return new Stacks
                {
                    id = item.id,
                    name = item.name,
                    size = item.size,
                };

            }

            return null;

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
