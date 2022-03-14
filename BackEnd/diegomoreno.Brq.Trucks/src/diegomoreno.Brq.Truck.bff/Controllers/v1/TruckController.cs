using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.Application.ViewModels.Trucks.Response;
using diegomoreno.Brq.bff.Attributes;
using diegomoreno.Brq.bff.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace diegomoreno.Brq.bff.Controllers.v1;

[ApiController]
public class TruckController : WebApiControllerBase
{
    private readonly ITruckAppService _appService;

    public TruckController(ITruckAppService appService)
    {
        _appService = appService;
    }

    [HttpGet]
    [BrqRoute(Routes.Trucks.GetAll)]
    [Produces("application/json", Type = typeof(List<GetTruckResponseViewModel>))]
    public async Task<IActionResult> GetAll()
    {
        var trucks = await _appService.GetAllAsync().ConfigureAwait(false);
        return Ok(trucks);
    }

    [HttpGet]
    [BrqRoute(Routes.Trucks.Get)]
    [Produces("application/json", Type = typeof(GetTruckResponseViewModel))]
    public async Task<IActionResult> Get([FromRoute] Guid idTruck)
    {
        var truck = await _appService.GetAsync(idTruck).ConfigureAwait(false);
        if (truck is null) return NotFound(idTruck);

        return Ok(truck);
    }

    [HttpPost]
    [BrqRoute(Routes.Trucks.Add)]
    [Produces("application/json", Type = typeof(TruckViewModel))]
    public async Task<IActionResult> AddAsync([FromBody] AddTruckRequestViewModel request)
    {
        return CustomResponse(await _appService.AddAsync(request).ConfigureAwait(false));
    }

    [HttpPut]
    [BrqRoute(Routes.Trucks.Update)]
    [Produces("application/json", Type = typeof(TruckViewModel))]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTruckRequestViewModel request, [FromRoute] Guid idTruck)
    {
        return CustomResponse(await _appService.UpdateAsync(request, idTruck).ConfigureAwait(false));
    }

    [HttpDelete]
    [BrqRoute(Routes.Trucks.Delete)]
    [Produces("application/json", Type = typeof(TruckViewModel))]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid idTruck)
    {
        await _appService.DeleteAsync(idTruck).ConfigureAwait(false);
        return Ok();
    }
}