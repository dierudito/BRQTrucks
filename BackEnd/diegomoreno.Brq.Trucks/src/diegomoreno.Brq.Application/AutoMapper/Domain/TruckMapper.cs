using AutoMapper;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.domain.Entities;
using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.Application.ViewModels.Trucks.Response;

namespace diegomoreno.Brq.Application.AutoMapper.Domain;

[ExcludeFromCodeCoverage]
public class TruckMapper : Profile
{
    public TruckMapper()
    {
        CreateMap<Truck, TruckViewModel>().ReverseMap();
        CreateMap<UpdateTruckRequestViewModel, TruckViewModel>();
        CreateMap<AddTruckRequestViewModel, TruckViewModel>();
        CreateMap<Truck, GetTruckResponseViewModel>()
            .ForMember(x => x.Series, o => o.MapFrom(src => src.Series.Name))
            .ForMember(x => x.IdSeries, o => o.MapFrom(src => src.Series.Id));
    }
}