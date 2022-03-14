using diegomoreno.Brq.Application.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.Application.ViewModels.Trucks;

[ExcludeFromCodeCoverage]
public class TruckViewModel : BaseViewModel
{
    public TruckViewModel()
    {
        Id = Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }
    public Guid IdSeries { get; set; }
    public int FabricationYear { get; set; }
    public int SerieYear { get; set; }
}