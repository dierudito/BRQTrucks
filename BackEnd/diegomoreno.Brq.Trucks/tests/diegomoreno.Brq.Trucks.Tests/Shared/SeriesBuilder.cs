using diegomoreno.Brq.domain.Entities;

namespace diegomoreno.Brq.Trucks.Tests.Shared;

public class SeriesBuilder
{
    public string Name { get; set; }

    public SeriesBuilder()
    {
        WithName("FH");
    }

    public SeriesBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public static SeriesBuilder Novo() => new SeriesBuilder();
    public Series Build() => new Series(Name);

}