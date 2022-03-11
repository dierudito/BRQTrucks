using System;
using Bogus;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Enums;
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
    public void ShouldNotAcceptToCreateAnInvalidTruck()
    {
        // Arrange / Act
        var truck = new Truck(SeriesEnum.FH, _faker.Date.Past().Year);

        // Assert
        Assert.False(truck.ItsValid());
    }

    [Fact]
    [Trait("Truck", "Entity")]
    public void ShouldAcceptToCreateTruckSuccessfully()
    {
        // Arrange / Act
        var truck = new Truck(SeriesEnum.FH, _faker.Date.Recent().Year);

        // Assert
        Assert.True(truck.ItsValid());
        Assert.Equal(SeriesEnum.FH, truck.SeriesEnum);
        Assert.Equal(_faker.Date.Recent().Year, truck.SerieYear);
        Assert.Equal(DateTime.UtcNow.Year, truck.FabricationYer);
    }
}