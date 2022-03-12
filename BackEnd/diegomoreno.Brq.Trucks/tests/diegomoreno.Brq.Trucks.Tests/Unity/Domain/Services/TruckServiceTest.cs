using System;
using Bogus;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.domain.Services;
using diegomoreno.Brq.Trucks.Tests.Shared;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Domain.Services;

public class TruckServiceTest
{
    private readonly Mock<ITruckRepository> _mockRepository;
    private readonly TruckService _truckService;
    private readonly Faker _faker;

    public TruckServiceTest()
    {
        _faker = new Faker();
        var mocker = new AutoMocker();

        _mockRepository = mocker.GetMock<ITruckRepository>();
        _truckService = mocker.CreateInstance<TruckService>();
    }


    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldNotAcceptToAddTruckWithAnInvalidSeriesYear()
    {
        // Arrange
        var truck =
            TruckBuilder
             .Novo()
             .WithSerieYear(_faker.Date.Past(3).Year)
             .Build();

        // Act
        await _truckService.AddAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x => x.AddAsync(It.IsAny<Truck>()), Times.Never);
    }


    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldAddTruckSuccessfully()
    {
        // Arrange
        var truck = TruckBuilder.Novo().Build();

        // Act
        await _truckService.AddAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x =>
            x.AddAsync(It.Is<Truck>(a =>
                a.SerieYear == truck.SerieYear &&
                a.FabricationYear == truck.FabricationYear &&
                a.SeriesEnum == truck.SeriesEnum)), Times.Once);
    }


    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldNotAcceptToUpdateTruckWithAnInvalidSeriesYear()
    {
        // Arrange
        var truck =
            TruckBuilder
                .Novo()
                .WithSerieYear(_faker.Date.Past(3).Year)
                .Build();

        // Act
        await _truckService.UpdateAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Truck>()), Times.Never);
    }


    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldUpdateTruckSuccessfully()
    {
        // Arrange
        var truck = TruckBuilder.Novo().Build();

        // Act
        await _truckService.UpdateAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x =>
            x.UpdateAsync(It.Is<Truck>(a =>
                a.SerieYear == truck.SerieYear &&
                a.FabricationYear == truck.FabricationYear &&
                a.SeriesEnum == truck.SeriesEnum)), Times.Once);
    }


    [Fact]
    [Trait("Truck", "Delete")]
    public async Task ShouldDeleteTruckSuccessfully()
    {
        // Assert
        var id = Guid.NewGuid();

        // Act
        await _truckService.DeleteAsync(id).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x =>
            x.DeleteAsync(id), Times.Once);
    }
}