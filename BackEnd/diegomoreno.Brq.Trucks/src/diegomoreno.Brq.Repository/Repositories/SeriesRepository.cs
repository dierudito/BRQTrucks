using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.Repository.Contexts.Entity;
using diegomoreno.Brq.Repository.Repositories.Base;

namespace diegomoreno.Brq.Repository.Repositories;

[ExcludeFromCodeCoverage]
public class SeriesRepository : BaseRepository<Series>, ISeriesRepository
{
    public SeriesRepository(TrucksDbContext db) : base(db)
    {
    }
}