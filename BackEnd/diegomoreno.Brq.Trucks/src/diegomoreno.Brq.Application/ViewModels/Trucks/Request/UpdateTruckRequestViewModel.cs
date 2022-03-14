using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.Application.ViewModels.Trucks;

[ExcludeFromCodeCoverage]
public class UpdateTruckRequestViewModel : TruckRequestViewModel
{
    [Key]
    public Guid Id { get; set; }
    public int FabricationYer { get; set; }
}