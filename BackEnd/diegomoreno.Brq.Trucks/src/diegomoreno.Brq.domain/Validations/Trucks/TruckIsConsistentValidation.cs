using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Specifications.Trucks;
using DomainValidation.Validation;

namespace diegomoreno.Brq.domain.Validations.Trucks;

public sealed class TruckIsConsistentValidation : Validator<Truck>
{
    public TruckIsConsistentValidation()
    {
        var truckSerieDate = new TruckShouldHaveValidSerieDate();
        var truckSeries = new TruckShouldBeOfSpecificSeries();

        Add("TruckSerieDate", new Rule<Truck>(truckSerieDate, "The series year should be the current or subsequent year."));
        Add("TruckSeries", new Rule<Truck>(truckSeries, "Truck series not allowed."));
    }
}