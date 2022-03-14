using diegomoreno.Brq.domain.Entities;

namespace diegomoreno.Brq.domain.Interfaces.Repositories.Base;

public interface IBaseRepository<T> : IAsyncDisposable where T : Entity
{
    Task<T> AddAsync(T entidade);
    Task<T> UpdateAsync(T entidade);
    Task<T?> UpdateAsync(T? updated, int key);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetById(Guid id);
}