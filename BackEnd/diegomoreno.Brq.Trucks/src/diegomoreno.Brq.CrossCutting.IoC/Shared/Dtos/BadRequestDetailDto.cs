using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.CrossCutting.IoC.Shared.Dtos;

[ExcludeFromCodeCoverage]
public class BadRequestDetailDto
{
    public string Descricao { get; init; }

    public BadRequestDetailDto() { }

    public BadRequestDetailDto(string descricao)
    {
        Descricao = descricao;
    }
}