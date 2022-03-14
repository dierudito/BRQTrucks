using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.CrossCutting.IoC.Shared.Dtos;

[ExcludeFromCodeCoverage]
public class BadRequestDto : List<BadRequestDetailDto>
{
    public BadRequestDto() { }
    public BadRequestDto(int capacity) : base(capacity) { }
    public BadRequestDto(IEnumerable<BadRequestDetailDto> collection) : base(collection) { }
    public BadRequestDto(params BadRequestDetailDto[] collection) : this((IEnumerable<BadRequestDetailDto>)collection) { }
}