using AutoMapper;
using diegomoreno.Brq.Application.AppService.Base;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Series;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using diegomoreno.Brq.domain.Interfaces.Repositories;

namespace diegomoreno.Brq.Application.AppService;

public class SeriesAppService : BaseAppService, ISeriesAppService
{
    private readonly ISeriesRepository _seriesRepository;

    public SeriesAppService(
        IUnitOfWork uow, 
        IMapper mapper, 
        ISeriesRepository seriesRepository) : base(uow, mapper)
    {
        _seriesRepository = seriesRepository;
    }

    public async Task<SeriesViewModel> GetAsync(Guid id) =>
        Mapper.Map<SeriesViewModel>(await _seriesRepository.GetById(id).ConfigureAwait(false));

    public async Task<IEnumerable<SeriesViewModel>> GetAllAsync() =>
    Mapper.Map<IEnumerable<SeriesViewModel>>(await _seriesRepository.GetAllAsync().ConfigureAwait(false));

    public async ValueTask DisposeAsync()
    {
        await _seriesRepository.DisposeAsync();
    }
}