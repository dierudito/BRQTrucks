using diegomoreno.Brq.domain.Validations.Trucks;

namespace diegomoreno.Brq.domain.Entities;

public class Truck : Entity
{
    public Guid IdSeries { get; private set; }
    public int FabricationYear { get; init; }
    public int SerieYear { get; private set; }

    public virtual Series Series { get; set; }

    public Truck()
    {
        FabricationYear = DateTime.UtcNow.Year;
    }

    public Truck(Series series, int serieYear) : this()
    {
        SetSeries(series);
        SetSerieYear(serieYear);
    }

    public void SetSeries(Series series)
    {
        IdSeries = series.Id;
        Series = series;
    }

    public void SetSerieYear(int serieYear)
    {
        SerieYear = serieYear;
    }

    public override bool ItsValid()
    {
        ValidationResult = new TruckIsConsistentValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}