using diegomoreno.Brq.domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace diegomoreno.Brq.domain.Specifications.Trucks;

public class TruckMustHaveValidSerieDate : ISpecification<Truck>
{
    public bool IsSatisfiedBy(Truck truck) =>
        truck.SerieYear == DateTime.UtcNow.Year || 
        truck.SerieYear == DateTime.UtcNow.AddYears(1).Year;
}