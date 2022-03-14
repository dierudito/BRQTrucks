using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.Application.AppService;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.CrossCutting.IoC.Configurations;

namespace diegomoreno.Brq.bff;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
            .ResolveDependencies()
            .AddAppServices();

    private static IServiceCollection AddAppServices(this IServiceCollection services) =>
        services
            .AddTransient<ITruckAppService, TruckAppService>()
            .AddTransient<ISeriesAppService, SeriesAppService>();
}