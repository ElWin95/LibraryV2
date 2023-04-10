using ConsoleAppLibraryV1.Models;

namespace ConsoleAppLibraryV1.Storage
{
    [Serializable]
    public class Database
    {
        public GenericStore<Authors> Author { get; set; }
        public GenericStore<Books> Book { get; set; }
    }
}
