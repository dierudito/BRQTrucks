using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Series;
using diegomoreno.Brq.bff.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace diegomoreno.Brq.bff.Controllers.v1;

[ApiController]
public class SeriesController : Controller
{
    private readonly ISeriesAppService _seriesAppService;

    public SeriesController(ISeriesAppService seriesAppService)
    {
        _seriesAppService = seriesAppService;
    }

    [HttpGet]
    [BrqRoute(Routes.Series.GetAll)]
    [Produces("application/json", Type = typeof(List<SeriesViewModel>))]
    public async Task<IActionResult> GetAll()
    {
        var series = await _seriesAppService.GetAllAsync().ConfigureAwait(false);
        return Ok(series);
    }

    [HttpGet]
    [BrqRoute(Routes.Series.Get)]
    [Produces("application/json", Type = typeof(SeriesViewModel))]
    public async Task<IActionResult> Get([FromRoute] Guid idSeries)
    {
        var series = await _seriesAppService.GetAsync(idSeries).ConfigureAwait(false);
        if (series is null) return NotFound(idSeries);

        return Ok(series);
    }
}