using System.Collections;
using System.Diagnostics;
using System.Text;
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

            Stacks stacks = new()
            {
                name = stackName
            };

            DBmanager.PostStack(stacks);

            return true;

        }

        internal static bool ProcessUpdate(Stacks currentStack)
        {

            Stacks stacks = new()
            {
                id = currentStack.id,
                name = currentStack.name,
                size = currentStack.size
            };

            DBmanager.UpdateStack(stacks);

            return true;

        }
        

        //For actually doing something with the DB since we need the id of the selected stack. This will not be used to display whatsoever.
        internal static List<Stacks> ProcessGetStack()
        {
            
            List<Stacks> allStacks = [];

            var Stack = DBmanager.GetStack();

            foreach (var item in Stack)
            {

                allStacks.Add(new Stacks
                {
                    id = item.id,
                    name = item.name,
                    size = item.size,
                });

            }

            return allStacks;

        }


        internal static Stacks? ProcessGetStackByID(int stackId)
        {

            var stack = DBmanager.GetStack();

            foreach (var item in stack)
            {
                if (!item.id.Equals(stackId)) 
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
            List<StackDTO> tableDisplay = [];

            var Stack = DBmanager.GetStack();


            foreach (var item in Stack)
            {

                StackDTO stack = new()
                {
                    nameDTO = item.name?.ToString(),
                    sizeDTO = item.size?.ToString() ?? "N/A",
                };

                tableDisplay.Add(stack);

            }

            return tableDisplay;

        }




        internal static void ProcessDeleteStack(Stacks currentStack)
        {

            int stackID = currentStack.id;
            DBmanager.DeleteStack(stackID);

        }



    }
}
