namespace diegomoreno.Brq.domain.Interfaces.Contexts.Entity;

public interface IContexto
{
    Task<bool> CommitAsync();
}