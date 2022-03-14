using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Domain.Services;
using diegomoreno.Brq.domain.Interfaces.Repositories;

namespace diegomoreno.Brq.domain.Services;

public class TruckService : ITruckService
{
    private readonly ITruckRepository _truckRepository;

    public TruckService(ITruckRepository truckRepository)
    {
        _truckRepository = truckRepository;
    }

    public async Task<Truck> AddAsync(Truck truck)
    {
        if(truck.ItsValid() is false) return truck;

        await _truckRepository.AddAsync(truck).ConfigureAwait(false);

        return truck;
    }

    public async Task<Truck> UpdateAsync(Truck truck)
    {
        if (truck.ItsValid() is false) return truck;

        await _truckRepository.UpdateAsync(truck).ConfigureAwait(false);

        return truck;
    }

    public async Task DeleteAsync(Guid id) => 
        await _truckRepository.DeleteAsync(id).ConfigureAwait(false);

    public async ValueTask DisposeAsync() => 
        await _truckRepository.DisposeAsync().ConfigureAwait(false);
}