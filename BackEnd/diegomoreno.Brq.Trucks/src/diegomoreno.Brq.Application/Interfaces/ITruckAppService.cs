using diegomoreno.Brq.Application.ViewModels;

namespace diegomoreno.Brq.Application.Interfaces;

public interface ITruckAppService : IAsyncDisposable
{
    Task<TruckViewModel> AddAsync(TruckViewModel truckViewModel);
    Task<TruckViewModel> UpdateAsync(TruckViewModel truckViewModel);
    Task DeleteAsync(Guid id);
    Task<TruckViewModel> GetAsync(Guid id);
    Task<IEnumerable<TruckViewModel>> GetAllAsync();
}