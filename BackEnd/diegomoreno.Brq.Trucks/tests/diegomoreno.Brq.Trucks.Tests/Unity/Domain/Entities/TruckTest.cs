using Bogus;
using diegomoreno.Brq.Trucks.Tests.Shared;
using System;
using Xunit;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Domain.Entities;

public class TruckTest
{
    private readonly Faker _faker;

    public TruckTest()
    {
        _faker = new Faker();
    }

    [Fact]
    [Trait("Truck", "Entity")]
    public void ShouldNotAcceptToCreateAnTruckWithInvalidSeriesYear()
    {
        // Arrange / Act
        var truck =
            TruckBuilder
                .Novo()
                .WithSerieYear(_faker.Date.Past(3).Year)
                .Build();

        // Assert
        Assert.False(truck.ItsValid());
    }

    [Fact]
    [Trait("Truck", "Entity")]
    public void ShouldNotAcceptToCreateAnTruckWithInvalidSeries()
    {
        // Arrange
        var series = SeriesBuilder.Novo().WithName("FMX").Build();

        // Act
        var truck =
            TruckBuilder
                .Novo()
                .WithSeries(series)
                .Build();

        // Assert
        Assert.False(truck.ItsValid());
    }

    [Theory]
    [InlineData("FM")]
    [InlineData("FH")]
    [Trait("Truck", "Entity")]
    public void ShouldAcceptToCreateTruckSuccessfully(string serie)
    {
        // Arrange
        var series = SeriesBuilder.Novo().WithName(serie).Build();

        // Act
        var truck = TruckBuilder.Novo().WithSeries(series).Build();

        // Assert
        Assert.True(truck.ItsValid());
        Assert.Equal(_faker.Date.Recent().Year, truck.SerieYear);
        Assert.Equal(DateTime.UtcNow.Year, truck.FabricationYear);
    }
}
