using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using diegomoreno.Brq.Application.ViewModels;
using diegomoreno.Brq.domain.Entities;

namespace diegomoreno.Brq.Application.AutoMapper.Domain;

[ExcludeFromCodeCoverage]
public class TruckMapper : Profile
{
    public TruckMapper()
    {
        CreateMap<Truck, TruckViewModel>().ReverseMap();
    }
}