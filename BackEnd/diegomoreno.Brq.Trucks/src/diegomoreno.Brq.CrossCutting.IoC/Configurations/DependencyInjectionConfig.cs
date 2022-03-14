using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using diegomoreno.Brq.domain.Interfaces.Domain.Services;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.domain.Services;
using diegomoreno.Brq.Repository.Contexts.Entity;
using diegomoreno.Brq.Repository.Repositories;
using diegomoreno.Brq.Repository.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace diegomoreno.Brq.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services) =>
        services
            .AddApplicationBase()
            .AddAutoMapper()
            .AddRepositories()
            .AddServices();

    private static IServiceCollection AddApplicationBase(this IServiceCollection services) =>
        services
            .AddScoped<TrucksDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddTransient<ITruckRepository, TruckRepository>()
            .AddTransient<ISeriesRepository, SeriesRepository>();

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<ITruckService, TruckService>();
}