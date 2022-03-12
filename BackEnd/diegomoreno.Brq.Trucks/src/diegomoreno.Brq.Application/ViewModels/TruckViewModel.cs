using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.Application.ViewModels.Base;
using diegomoreno.Brq.domain.Enums;

namespace diegomoreno.Brq.Application.ViewModels;

[ExcludeFromCodeCoverage]
public class TruckViewModel : BaseViewModel
{
    public TruckViewModel()
    {
        Id = Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }
    public SeriesEnum SeriesEnum { get; set; }
    public int FabricationYer { get; set; }
    public int SerieYear { get; set; }
}