using System.ComponentModel.DataAnnotations;

namespace diegomoreno.Brq.Application.ViewModels.Series;

public class SeriesViewModel
{
    public SeriesViewModel()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}