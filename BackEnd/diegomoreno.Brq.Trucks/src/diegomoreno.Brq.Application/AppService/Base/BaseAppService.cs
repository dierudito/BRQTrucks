using AutoMapper;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using DomainValidation.Validation;

namespace diegomoreno.Brq.Application.AppService.Base;

public abstract class BaseAppService
{
    private readonly IUnitOfWork _uow;
    protected readonly IMapper Mapper;

    protected BaseAppService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        Mapper = mapper;
    }

    protected void AddValidationErrors(ValidationResult? validationResult, string erro)
    {
        validationResult ??= new ValidationResult();
        validationResult.Add(new ValidationError(erro));
    }

    protected async Task<bool> CommitAsync() => await _uow.CommitAsync().ConfigureAwait(false);
}