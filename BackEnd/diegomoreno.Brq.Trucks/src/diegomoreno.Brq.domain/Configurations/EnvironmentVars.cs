using System.Diagnostics.CodeAnalysis;
using EnvVar = System.Collections.Generic.KeyValuePair<string, string?>;
namespace diegomoreno.Brq.domain.Configurations;

[ExcludeFromCodeCoverage]
public class EnvironmentVars
{

    protected static EnvVar NewEnvVar(string key) => new(key, Environment.GetEnvironmentVariable(key));

    protected static EnvVar GetEnvVar(params string[] strArr) => NewEnvVar(GetEnvVarName(strArr));

    protected static string GetEnvVarName(params string[] strArr) => string.Join('_', strArr).ToUpper();
    public static EnvVar ConnectionStrings => GetEnvVar(nameof(ConnectionStrings));
}