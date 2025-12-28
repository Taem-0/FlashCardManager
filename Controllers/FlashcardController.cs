
using FlashCardManager.DTO_s;
using FlashCardManager.FlashCardDB;
using FlashCardManager.Helpers;
using FlashCardManager.Models;
using Spectre.Console;

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




        internal static List<FlashCards> ProcessGetFlashcards()
        {
            List<FlashCards> flashCards = [];

            var flashCard = DBmanager.GetFlashCard();

            foreach (var item in flashCard)
            {

                flashCards.Add(new FlashCards
                {
                    id = item.id,
                    front = item.front,
                    back = item.back,
                    stackId = item.stackId
                });

            }

            return flashCards;

        }


        internal static List<FlashCards> ProcessGetFlashcardByID(int stackId)
        {

            List<FlashCards> flashCards = [];

            var flashCard = DBmanager.GetFlashCard();

            foreach (var item in flashCard)
            {
                if (!item.stackId.Equals(stackId))
                {
                    continue;
                }

                flashCards.Add(new FlashCards
                {
                    id = item.id,
                    front = item.front,
                    back = item.back,
                    stackId = item.stackId
                });

            }

            return flashCards;

        }


        internal static List<FlashcardDTO> ProcessGetFlashcardDTO(int stackID)
        {
            List<FlashcardDTO> tableDisplay = [];

            var flashCard = ProcessGetFlashcardByID(stackID);


            foreach (var item in flashCard)
            {

                FlashcardDTO flashCards = new()
                {
                    idDTO = item.id.ToString(),
                    frontDTO = item.front,
                    backDTO = item.back,
                    
                };

                tableDisplay.Add(flashCards);

            }

            return tableDisplay;

        }




        internal static void ProcessDeleteFlashcard(FlashCards flashCards)
        {

            int flashcardID = flashCards.id;
            DBmanager.DeleteFlashcard(flashcardID);

        }




        internal static bool ProcessUpdateFront(FlashCards flashCard, string update)
        {


            FlashCards flashcardUpdate = new()
            {

                id = flashCard.id,
                front = update,
                back = flashCard.back,
                stackId = flashCard.stackId,

            };

            DBmanager.UpdateFlashCard(flashcardUpdate);
            return true;

        }

        internal static bool ProcessUpdateBack(FlashCards flashCard, string update)
        {


            FlashCards flashcardUpdate = new()
            {

                id = flashCard.id,
                front = flashCard.front,
                back = update,
                stackId = flashCard.stackId,

            };

            DBmanager.UpdateFlashCard(flashcardUpdate);
            return true;

        }




    }
}
