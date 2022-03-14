using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace diegomoreno.Brq.Application.ViewModels.Base;

[ExcludeFromCodeCoverage]
public class BaseViewModel
{
    [ScaffoldColumn(false)]
    public ValidationResult? ValidationResult { get; set; }
}