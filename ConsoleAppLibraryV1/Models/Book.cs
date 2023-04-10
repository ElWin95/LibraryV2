using ConsoleAppLibraryV1.StableModels;
using ConsoleAppLibraryV1.Storage;

namespace ConsoleAppLibraryV1.Models
{
    [Serializable]
    public class Books : IIdentity
    {
        static int counter = 0;
        public Books()
        {
            counter++;
            this.Id = counter;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public int AuthorsId { get; set; }
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nGenre: {Genre}\nPageCount: {PageCount}\nPrice:{Price}";
        }
    }
}
