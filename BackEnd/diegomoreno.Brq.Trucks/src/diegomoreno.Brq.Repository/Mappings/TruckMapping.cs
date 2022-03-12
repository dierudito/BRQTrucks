using diegomoreno.Brq.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace diegomoreno.Brq.Repository.Mappings;

public class TruckMapping : IEntityTypeConfiguration<Truck>
{
    [ExcludeFromCodeCoverage]
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.ToTable("Trucks").HasKey(x => x.Id);
        builder.Property(x => x.SerieYear);
        builder.Property(x => x.FabricationYear);
        builder.Property(x => x.SeriesEnum).HasColumnType("varchar(3)");
    }
}