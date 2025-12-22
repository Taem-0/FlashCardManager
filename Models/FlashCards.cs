

namespace FlashCardManager.Models
{
    internal class FlashCards
    {

        public int id {  get; set; }

        public string? front { get; set; }

        public string? back { get; set; }

        public int stackId { get; set; }


        public static readonly FlashCards EmptyFlashCard = new()
        {

            id = -1,
            front = "[Empty]",
            back = "[Empty]",
            stackId = -1,

        };

    }
}
