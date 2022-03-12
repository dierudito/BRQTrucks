using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Enums;

namespace diegomoreno.Brq.Application.ViewModels.Trucks;

[ExcludeFromCodeCoverage]
public class UpdateTruckRequestViewModel
{
    [Key]
    public Guid Id { get; set; }
    public SeriesEnum SeriesEnum { get; set; }
    public int FabricationYer { get; set; }
    public int SerieYear { get; set; }
}