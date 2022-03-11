using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Interfaces.Contexts.Dapper;

namespace diegomoreno.Brq.Repository.Contexts.Dapper;

[ExcludeFromCodeCoverage]
public class DataFactory : IDataFactory
{
    public static Dictionary<string, string>? ConnectionStrings { get; set; }
    public static string? ConnectionDefault { get; set; }
    public static void SetConnectionString(Dictionary<string, string> connStrs)
    {
        ConnectionStrings = connStrs;
    }
    public static void SetConnectionStringDefault(string? conn)
    {
        ConnectionDefault = conn;
    }


    private static SqlConnection CreateConnection(string? conn = null)
    {
        return new SqlConnection(conn == null ? ConnectionDefault : ConnectionStrings?[conn]);
    }

    public async Task<IDbConnection> OpenConnectionAsync(string? conn = null)
    {
        var c = CreateConnection(conn);
        await c.OpenAsync().ConfigureAwait(false);
        return c;
    }
}