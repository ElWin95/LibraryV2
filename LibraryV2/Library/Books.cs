using LibraryV2.Category;
using LibraryV2.Storage.IIdentity;

namespace LibraryV2.Library
{
    [Serializable]
    public class Books : IIdentity
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int AuthorsId { get; set; }
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }

        static int counter;
        public Books()
        {
            this.Id = ++counter;
        }

        public override string ToString()
        {
            return $"Name: {Name}\nGenre: {Genre}\nPageCount: {PageCount}\nPrice:{Price}";
        }
    }
}
