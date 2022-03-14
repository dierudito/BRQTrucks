namespace diegomoreno.Brq.domain.Entities;

public class Series : Entity
{
    public string Name { get; init; }

    public Series(string name) : this()
    {
        Name = name;
    }

    public Series()
    {
    }

    //TODO There is no validation specification for this entity in the business rule
    public override bool ItsValid()
    {
        return true;
    }
}