using AutoMapper;
using diegomoreno.Brq.Application.AppService.Base;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using diegomoreno.Brq.domain.Interfaces.Domain.Services;
using diegomoreno.Brq.domain.Interfaces.Repositories;

namespace diegomoreno.Brq.Application.AppService;

public class TruckAppService : BaseAppService, ITruckAppService
{
    private readonly ITruckRepository _truckRepository;
    private readonly ITruckService _truckService;

    public TruckAppService(
        IUnitOfWork uow,
        IMapper mapper,
        ITruckRepository truckRepository, 
        ITruckService truckService) : base(uow, mapper)
    {
        _truckRepository = truckRepository;
        _truckService = truckService;
    }

    public async Task<TruckViewModel> GetAsync(Guid id) =>
        Mapper.Map<TruckViewModel>(await _truckRepository.GetById(id).ConfigureAwait(false));

    public async Task<IEnumerable<TruckViewModel>> GetAllAsync() =>
        Mapper.Map<IEnumerable<TruckViewModel>>(await _truckRepository.GetAllAsync().ConfigureAwait(false));

    public async Task<TruckViewModel> AddAsync(AddTruckRequestViewModel request)
    {
        var truckViewModel = Mapper.Map<TruckViewModel>(request);
        var truck = Mapper.Map<Truck>(truckViewModel);
        var response = await _truckService.AddAsync(truck).ConfigureAwait(false);

        if(response is {ValidationResult.IsValid: true}) 
            if(!await CommitAsync())
                AddValidationErrors(truck.ValidationResult, "An error occurred while saving the data in the database.");
        truckViewModel.ValidationResult = truck.ValidationResult;

        return truckViewModel;
    }

    public async Task<TruckViewModel> UpdateAsync(UpdateTruckRequestViewModel request)
    {
        var truckViewModel = Mapper.Map<TruckViewModel>(request);
        var truck = Mapper.Map<Truck>(truckViewModel);
        var response = await _truckService.UpdateAsync(truck).ConfigureAwait(false);

        if (response is { ValidationResult.IsValid: true})
            if (!await CommitAsync())
                AddValidationErrors(truck.ValidationResult, "An error occurred while saving the data in the database.");

        truckViewModel.ValidationResult = truck.ValidationResult;

        return truckViewModel;
    }

    public async Task DeleteAsync(Guid id) => 
        await _truckService.DeleteAsync(id).ConfigureAwait(false);

    public async ValueTask DisposeAsync()
    {
        await _truckRepository.DisposeAsync();
        await _truckService.DisposeAsync();
    }
}