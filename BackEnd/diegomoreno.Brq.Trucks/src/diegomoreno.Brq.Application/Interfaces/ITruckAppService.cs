using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.Application.ViewModels.Trucks.Response;

namespace diegomoreno.Brq.Application.Interfaces;

public interface ITruckAppService : IAsyncDisposable
{
    Task<TruckViewModel> AddAsync(AddTruckRequestViewModel request);
    Task<TruckViewModel> UpdateAsync(UpdateTruckRequestViewModel request, Guid idTruck);
    Task DeleteAsync(Guid id);
    Task<GetTruckResponseViewModel> GetAsync(Guid id);
    Task<IEnumerable<GetTruckResponseViewModel>> GetAllAsync();
}