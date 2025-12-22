using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCardManager.FlashCardDB;
using FlashCardManager.Models;

namespace FlashCardManager.Controllers
{
    internal class FlashcardController
    {


        internal static bool ProcessAdd(string frontSide, string backSide, Stacks stacks)
        {

            FlashCards flashCards = new()
            {
                front = frontSide,
                back = backSide,
                stackId = stacks.id
            };

            DBmanager.PostFlashCard(flashCards);

            return true;

        } 


    }
}
