namespace diegomoreno.Brq.domain.Entities;

public class DomainException : Exception
{
    public string PropriedadeNome { get; set; }
    public string ValorLimite { get; set; }
    public string ValorLimite2 { get; set; }
    public DomainException(string message, string propriedadeNome) : base(message)
    {
        PropriedadeNome = propriedadeNome;
    }
    public DomainException(string message, string propriedadeNome, string valorLimite) : base(message)
    {
        PropriedadeNome = propriedadeNome;
        ValorLimite = valorLimite;
    }
    public DomainException(string message, string propriedadeNome, string valorLimite, string valorLimite2) : base(message)
    {
        PropriedadeNome = propriedadeNome;
        ValorLimite = valorLimite;
        ValorLimite2 = valorLimite2;
    }
}