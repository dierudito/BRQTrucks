using System.Data;

namespace diegomoreno.Brq.domain.Interfaces.Contexts.Dapper;

public interface IDataFactory
{
    Task<IDbConnection> OpenConnectionAsync(string? conn = null);
}