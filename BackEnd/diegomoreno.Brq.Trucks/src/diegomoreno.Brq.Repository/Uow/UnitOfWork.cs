using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using diegomoreno.Brq.Repository.Contexts.Entity;

namespace diegomoreno.Brq.Repository.Uow;

[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly TrucksDbContext _db;

    public UnitOfWork(TrucksDbContext db)
    {
        _db = db;
    }

    public async Task<bool> CommitAsync() => 
        await _db.SaveChangesAsync().ConfigureAwait(false) > 0;

}