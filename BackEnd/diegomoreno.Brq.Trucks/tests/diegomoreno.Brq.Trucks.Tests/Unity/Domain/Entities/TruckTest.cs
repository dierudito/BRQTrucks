using System;
using Bogus;
using diegomoreno.Brq.domain.Enums;
using diegomoreno.Brq.Trucks.Tests.Shared;
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
    public void ShouldAcceptToCreateTruckSuccessfully()
    {
        // Arrange / Act
        var truck = TruckBuilder.Novo().Build();

        // Assert
        Assert.True(truck.ItsValid());
        Assert.Equal(SeriesEnum.FH, truck.SeriesEnum);
        Assert.Equal(_faker.Date.Recent().Year, truck.SerieYear);
        Assert.Equal(DateTime.UtcNow.Year, truck.FabricationYear);
    }
}