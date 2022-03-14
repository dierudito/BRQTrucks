using System.Diagnostics.CodeAnalysis;
using System.Net;
using diegomoreno.Brq.Application.ViewModels.Base;
using diegomoreno.Brq.CrossCutting.IoC.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace diegomoreno.Brq.bff.Controllers.Base;

[ExcludeFromCodeCoverage]
[ApiController]
public class WebApiControllerBase : ControllerBase
{
    protected ActionResult CustomResponse(BaseViewModel? result = null, HttpStatusCode? statusResponse = null)
    {
        if (result is null)
            statusResponse = HttpStatusCode.NotFound;
        else
            statusResponse ??= result.ValidationResult == null || result.ValidationResult.IsValid ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

        return StatusCode(statusResponse.GetHashCode(), result);
    }

    private static BadRequestDto? ReturnRequestFailureObject(DomainValidation.Validation.ValidationResult? validationResult)
    {
        if (validationResult is null) return null;
        var detail = validationResult.Erros.Select(error => new BadRequestDetailDto(error.Message)).ToList();


        return detail.Count == 0 ? null : new BadRequestDto(detail);
    }
}