namespace diegomoreno.Brq.domain.Interfaces.Contexts.Entity;

public interface IContexto
{
    int Commit();
    Task<int> CommitAsync();
}