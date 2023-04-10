using ConsoleAppLibraryV1.Storage;

namespace ConsoleAppLibraryV1.Models;
[Serializable]
public class Authors : IIdentity, IEquatable<Authors>
{
    static int counter = 0;
    public Authors()
    {
        counter++;
        this.Id = counter;
    }

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public bool Equals(Authors? other)
    {
        return this.Id == other.Id;
    }

    public override string ToString()
    {
        return $"{Id}.{Name} {Surname}";
    }

}
