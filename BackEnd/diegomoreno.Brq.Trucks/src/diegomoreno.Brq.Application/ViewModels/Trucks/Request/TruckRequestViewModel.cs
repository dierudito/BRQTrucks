using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.Application.ViewModels.Trucks;

[ExcludeFromCodeCoverage]
public class TruckRequestViewModel
{
    public Guid IdSeries { get; set; }
    public int SerieYear { get; set; }
}