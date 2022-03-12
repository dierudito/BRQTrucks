using diegomoreno.Brq.domain.Enums;
using diegomoreno.Brq.domain.Validations.Trucks;

namespace diegomoreno.Brq.domain.Entities;

public class Truck : Entity
{
    public SeriesEnum SeriesEnum { get; init; }
    public int FabricationYear { get; init; }
    public int SerieYear { get; init; }

    public Truck()
    {
        FabricationYear = DateTime.UtcNow.Year;
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