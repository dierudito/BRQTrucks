using diegomoreno.Brq.Trucks.Tests.Shared;
using FluentAssertions;
using Xunit;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Domain.Entities;

public class SeriesTest
{
    [Fact]
    [Trait("Series", "Entity")]
    public void ShouldCreateSeries()
    {
        // Arrange / Act
        var series = SeriesBuilder.Novo().WithName("FM").Build();

        // Assert
        series.Should().NotBeNull();
        Assert.True(series.ItsValid());
        Assert.Equal("FM", series.Name);
    }
}