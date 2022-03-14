using AutoMapper;
using diegomoreno.Brq.Application.AppService.Base;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.Application.ViewModels.Trucks.Response;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using diegomoreno.Brq.domain.Interfaces.Domain.Services;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using DomainValidation.Validation;

namespace diegomoreno.Brq.Application.AppService;

public class TruckAppService : BaseAppService, ITruckAppService
{
    private readonly ITruckRepository _truckRepository;
    private readonly ITruckService _truckService;
    private readonly ISeriesRepository _seriesRepository;

    public TruckAppService(
        IUnitOfWork uow,
        IMapper mapper,
        ITruckRepository truckRepository, 
        ITruckService truckService, 
        ISeriesRepository seriesRepository) : base(uow, mapper)
    {
        _truckRepository = truckRepository;
        _truckService = truckService;
        _seriesRepository = seriesRepository;
    }

    public async Task<GetTruckResponseViewModel> GetAsync(Guid id) =>
        Mapper.Map<GetTruckResponseViewModel>(await _truckRepository.GetById(id).ConfigureAwait(false));

    public async Task<IEnumerable<GetTruckResponseViewModel>> GetAllAsync() =>
        Mapper.Map<IEnumerable<GetTruckResponseViewModel>>(await _truckRepository.GetAllAsync().ConfigureAwait(false));

    public async Task<TruckViewModel> AddAsync(AddTruckRequestViewModel request)
    {
        var truckViewModel = Mapper.Map<TruckViewModel>(request);
        var series = await _seriesRepository.GetById(truckViewModel.IdSeries).ConfigureAwait(false);

        if (series == null)
        {
            truckViewModel.ValidationResult.Add(new ValidationError("Series not found"));
            return truckViewModel;
        }
        var truck = new Truck(series, truckViewModel.SerieYear);
        var response = await _truckService.AddAsync(truck).ConfigureAwait(false);

        if(response is {ValidationResult.IsValid: true}) 
            if(!await CommitAsync().ConfigureAwait(false))
                AddValidationErrors(truck.ValidationResult, "An error occurred while saving the data in the database.");
        truckViewModel.ValidationResult = truck.ValidationResult;

        return truckViewModel;
    }

    public async Task<TruckViewModel> UpdateAsync(UpdateTruckRequestViewModel request, Guid idTruck)
    {
        var truckViewModel = Mapper.Map<TruckViewModel>(request);

        var series = await _seriesRepository.GetById(truckViewModel.IdSeries).ConfigureAwait(false);
        if (series == null)
        {
            truckViewModel.ValidationResult.Add(new ValidationError("Series not found"));
            return truckViewModel;
        }

        var truck = await _truckRepository.GetById(idTruck);
        if (truck == null)
        {
            truckViewModel.ValidationResult.Add(new ValidationError("Truck not found"));
            return truckViewModel;
        }
        truck.SetSeries(series);
        truck.SetSerieYear(truckViewModel.SerieYear);
        
        var response = await _truckService.UpdateAsync(truck).ConfigureAwait(false);

        if (response is { ValidationResult.IsValid: true})
            if (!await CommitAsync().ConfigureAwait(false))
                AddValidationErrors(truck.ValidationResult, "An error occurred while saving the data in the database.");

        truckViewModel.ValidationResult = truck.ValidationResult;

        return truckViewModel;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _truckService.DeleteAsync(id).ConfigureAwait(false);
        await CommitAsync().ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await _truckRepository.DisposeAsync();
        await _truckService.DisposeAsync();
    }
}