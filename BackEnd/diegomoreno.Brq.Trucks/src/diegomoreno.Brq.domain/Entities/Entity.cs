using DomainValidation.Validation;

namespace diegomoreno.Brq.domain.Entities;

public abstract class Entity
{
    public Guid Id { get; init; }
    public ValidationResult ValidationResult { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public void AddValidationError(string erro, string mensagem)
    {
        ValidationResult.Add(new ValidationError(mensagem));
    }

    public void AddValidationErrors(ValidationResult validationResult)
    {
        ValidationResult.Add(validationResult);
    }

    public void ClearErrorList()
    {
        ValidationResult = new ValidationResult();
    }

    public abstract bool ItsValid();
}