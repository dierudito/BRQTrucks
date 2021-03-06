using DomainValidation.Validation;

namespace diegomoreno.Brq.domain.Entities;

public abstract class Entity
{
    public Guid Id { get; init; }
    public ValidationResult? ValidationResult { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        ValidationResult = new ValidationResult();
    }

    public abstract bool ItsValid();
}