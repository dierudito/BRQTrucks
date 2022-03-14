using diegomoreno.Brq.domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace diegomoreno.Brq.domain.Specifications.Trucks;

public class TruckShouldBeOfSpecificSeries : ISpecification<Truck>
{
    public bool IsSatisfiedBy(Truck truck) =>
        truck.Series.Name.Equals("FM") ||
        truck.Series.Name.Equals("FH");
}