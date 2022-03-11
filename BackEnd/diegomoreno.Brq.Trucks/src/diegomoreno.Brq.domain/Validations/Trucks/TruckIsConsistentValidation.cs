using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Specifications.Trucks;
using DomainValidation.Validation;

namespace diegomoreno.Brq.domain.Validations.Trucks;

public sealed class TruckIsConsistentValidation : Validator<Truck>
{
    public TruckIsConsistentValidation()
    {
        var truckSerieDate = new TruckMustHaveValidSerieDate();

        Add("TruckSerieDate", new Rule<Truck>(truckSerieDate, "The series year must be the current or subsequent year."));
    }
}