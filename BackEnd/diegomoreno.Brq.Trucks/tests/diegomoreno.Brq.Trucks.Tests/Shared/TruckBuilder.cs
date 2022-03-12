using Bogus;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Enums;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace diegomoreno.Brq.Trucks.Tests.Shared;

public class TruckBuilder
{
    public SeriesEnum SeriesEnum { get; set; }
    public int SerieYear { get; set; }
    public ValidationResult ValidationResult { get; set; }

    public TruckBuilder()
    {
        var faker = new Faker();
        WithSerieYear(faker.Date.Recent().Year);
        WithSeriesEnum(SeriesEnum.FH);

        ValidationResult validationResult = new();
        validationResult.Add(new ValidationResult());
        WithValidationResult(validationResult);
    }

    public static TruckBuilder Novo() => new();

    public TruckBuilder WithSeriesEnum(SeriesEnum seriesEnum)
    {
        SeriesEnum = seriesEnum;
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

    public Truck Build() => new(SeriesEnum, SerieYear)
    {
        ValidationResult = ValidationResult
    };
}