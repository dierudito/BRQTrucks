using diegomoreno.Brq.Application.ViewModels.Series;

namespace diegomoreno.Brq.Application.Interfaces;

public interface ISeriesAppService : IAsyncDisposable
{
    Task<SeriesViewModel> GetAsync(Guid id);
    Task<IEnumerable<SeriesViewModel>> GetAllAsync();
}