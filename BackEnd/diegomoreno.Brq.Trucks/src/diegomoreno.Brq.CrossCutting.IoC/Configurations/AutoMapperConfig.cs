using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using diegomoreno.Brq.Application.AutoMapper.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace diegomoreno.Brq.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class AutoMapperConfig
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AllowNullDestinationValues = true;

            mc.AddProfile(new TruckMapper());
        });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}