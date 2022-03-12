using diegomoreno.Brq.domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.Application.ViewModels.Trucks;

[ExcludeFromCodeCoverage]
public class AddTruckRequestViewModel
{
    public SeriesEnum SeriesEnum { get; set; }
    public int SerieYear { get; set; }
}