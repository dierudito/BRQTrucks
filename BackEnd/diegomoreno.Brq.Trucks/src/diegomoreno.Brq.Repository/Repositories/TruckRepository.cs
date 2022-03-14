using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.Repository.Contexts.Entity;
using diegomoreno.Brq.Repository.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace diegomoreno.Brq.Repository.Repositories;

[ExcludeFromCodeCoverage]
public class TruckRepository : BaseRepository<Truck>, ITruckRepository
{
    public TruckRepository(TrucksDbContext db) : base(db)
    {
    }

    public override async Task<Truck?> GetById(Guid id) =>
        await _db.Trucks
            .Include(t => t.Series)
            .FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

    public override async Task<IEnumerable<Truck>> GetAllAsync() =>
        await _db.Trucks
            .Include(t => t.Series)
            .ToListAsync().ConfigureAwait(false);
}