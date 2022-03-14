using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace diegomoreno.Brq.Repository.Mappings;

public class SeriesMapping : IEntityTypeConfiguration<Series>
{
    [ExcludeFromCodeCoverage]
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.ToTable("Series").HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType("varchar(2)");
    }
}