using LibraryV2.Storage.IIdentity;

namespace LibraryV2;
[Serializable]
public class Authors : IIdentity, IEquatable<Authors>
{

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }


    static int counter;
    public Authors()
    {
        this.Id = ++counter;
    }

    public override string ToString()
    {
        return $"{Id}.{Name} {Surname}";
    }

    public bool Equals(Authors? other)
    {
        return this.Id == other.Id;
    }


}
