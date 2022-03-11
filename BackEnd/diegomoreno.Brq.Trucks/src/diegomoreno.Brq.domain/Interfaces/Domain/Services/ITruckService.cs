using diegomoreno.Brq.domain.Entities;

namespace diegomoreno.Brq.domain.Interfaces.Domain.Services;

public interface ITruckService : IAsyncDisposable
{
    Task<Truck> AddAsync(Truck truck);
    Task<Truck> UpdateAsync(Truck truck);
    Task DeleteAsync(Guid id);
}