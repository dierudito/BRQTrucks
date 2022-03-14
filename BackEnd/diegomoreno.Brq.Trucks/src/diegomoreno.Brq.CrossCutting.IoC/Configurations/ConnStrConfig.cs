using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.CrossCutting.IoC.Shared.Extensions;
using diegomoreno.Brq.domain.Configurations;

namespace diegomoreno.Brq.CrossCutting.IoC.Configurations;

[ExcludeFromCodeCoverage]
public static class ConnStrConfig
{
    private class ConnectionString
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    private const string DefaultConnStrName = "Default";

    public static Dictionary<string, string> AppConnections
    {
        get
        {
            var list = EnvironmentVars.ConnectionStrings.Value.FromJsonTo<List<ConnectionString>>();

            var dict = list.ToDictionary(
                x => x.Name,
                x => x.Value
            );

            return dict;
        }
    }

    public static string DefaultConnectionString => AppConnections[DefaultConnStrName];
}