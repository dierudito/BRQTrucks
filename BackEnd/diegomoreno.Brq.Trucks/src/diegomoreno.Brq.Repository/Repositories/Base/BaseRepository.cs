using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Repositories.Base;
using diegomoreno.Brq.Repository.Contexts.Entity;
using Microsoft.EntityFrameworkCore;

namespace diegomoreno.Brq.Repository.Repositories.Base;

[ExcludeFromCodeCoverage]
public class BaseRepository<T> : IBaseRepository<T> where T : Entity, new()
{
    protected TrucksDbContext _db;
    protected DbSet<T> _dbSet;

    public BaseRepository(TrucksDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }

    public async Task<T> AddAsync(T entidade)
    {
        await _db.Set<T>().AddAsync(entidade).ConfigureAwait(false);
        return entidade;
    }

    public Task<T> UpdateAsync(T entidade)
    {
        _db.Set<T>().Update(entidade);
        return Task.FromResult(entidade);
    }

    public async Task<T?> UpdateAsync(T? updated, int key)
    {
        if (updated == null) return null;

        var existing = await _db.Set<T>().FindAsync(key).ConfigureAwait(false);
        if (existing != null) _db.Entry(existing).CurrentValues.SetValues(updated);
        return existing;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetById(id).ConfigureAwait(false);
        if (entity != null) _db.Set<T>().Remove(entity);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() =>
        await _db.Set<T>().ToListAsync().ConfigureAwait(false);


    public virtual async Task<T?> GetById(Guid id)
    {
        var result = await _db.Set<T>().FindAsync(id).ConfigureAwait(false);
        return result;
    }

    public async ValueTask DisposeAsync()
    {
        await _db.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}