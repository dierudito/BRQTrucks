using Bogus;
using diegomoreno.Brq.domain.Entities;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace diegomoreno.Brq.Trucks.Tests.Shared;

public class TruckBuilder
{
    public Series Series { get; set; }
    public int SerieYear { get; set; }
    public ValidationResult ValidationResult { get; set; }

    public TruckBuilder()
    {
        var faker = new Faker();
        WithSerieYear(faker.Date.Recent().Year);
        WithSeries(SeriesBuilder.Novo().Build());

        ValidationResult validationResult = new();
        validationResult.Add(new ValidationResult());
        WithValidationResult(validationResult);
    }

    public static TruckBuilder Novo() => new();

    public TruckBuilder WithSeries(Series series)
    {
        Series = series;
        return this;
    }

    public TruckBuilder WithSerieYear(int serieYear)
    {
        SerieYear = serieYear;
        return this;
    }

    public TruckBuilder WithValidationResult(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
        return this;
    }

    public Truck Build() => new(Series, SerieYear)
    {
        ValidationResult = ValidationResult
    };
}