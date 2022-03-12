using diegomoreno.Brq.Application.ViewModels.Trucks;

namespace diegomoreno.Brq.Application.Interfaces;

public interface ITruckAppService : IAsyncDisposable
{
    Task<TruckViewModel> AddAsync(AddTruckRequestViewModel request);
    Task<TruckViewModel> UpdateAsync(UpdateTruckRequestViewModel request);
    Task DeleteAsync(Guid id);
    Task<TruckViewModel> GetAsync(Guid id);
    Task<IEnumerable<TruckViewModel>> GetAllAsync();
}