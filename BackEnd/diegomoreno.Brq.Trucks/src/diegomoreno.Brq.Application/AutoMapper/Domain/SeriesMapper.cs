using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using diegomoreno.Brq.Application.ViewModels.Series;
using diegomoreno.Brq.domain.Entities;

namespace diegomoreno.Brq.Application.AutoMapper.Domain;

[ExcludeFromCodeCoverage]
public class SeriesMapper : Profile
{
    public SeriesMapper()
    {
        CreateMap<Series, SeriesViewModel>().ReverseMap();
    }
}