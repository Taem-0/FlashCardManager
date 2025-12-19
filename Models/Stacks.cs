
namespace FlashCardManager.Models
{
    internal class Stacks
    {

        public int id { get; set; }

        public string? name { get; set; }

        public int? size { get; set; }



        public static readonly Stacks EmptyStack = new()
        {
            id = -1,
            name = "[Empty Stack]",
            size = 0,
        };   

    }
}
