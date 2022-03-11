using diegomoreno.Brq.domain.Enums;
using diegomoreno.Brq.domain.Validations.Trucks;

namespace diegomoreno.Brq.domain.Entities;

public class Truck : Entity
{
    public SeriesEnum SeriesEnum { get; private init; }
    public int FabricationYer { get; init; }
    public int SerieYear { get; private set; }

    public Truck()
    {
        FabricationYer = DateTime.UtcNow.Year;
    }

    public Truck(SeriesEnum seriesEnum, int serieYear) : this()
    {
        SeriesEnum = seriesEnum;
        SerieYear = serieYear;
    }

    public override bool ItsValid()
    {
        ValidationResult = new TruckIsConsistentValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}