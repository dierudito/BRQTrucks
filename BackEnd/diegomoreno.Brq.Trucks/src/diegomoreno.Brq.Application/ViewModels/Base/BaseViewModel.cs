using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.Application.ViewModels.Base;

[ExcludeFromCodeCoverage]
public class BaseViewModel
{
    [ScaffoldColumn(false)]
    public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }
}