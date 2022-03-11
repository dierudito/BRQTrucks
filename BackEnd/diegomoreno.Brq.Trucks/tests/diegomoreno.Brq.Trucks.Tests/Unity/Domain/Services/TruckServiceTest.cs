using System.Threading.Tasks;
using Bogus;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Enums;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.domain.Services;
using Moq;
using Moq.AutoMock;
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
        var autoMocker = new AutoMocker();

        _mockRepository = autoMocker.GetMock<ITruckRepository>();
        _truckService = autoMocker.CreateInstance<TruckService>();
    }


    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldNotAcceptToAddTruckWithAnInvalidSeriesYear()
    {
        // Arrange
        var truck = new Truck(SeriesEnum.FH, _faker.Date.Past(1).Year);

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
        var truck = new Truck(SeriesEnum.FH, _faker.Date.Recent().Year);

        // Act
        await _truckService.AddAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x => 
            x.AddAsync(It.Is<Truck>(a => 
                a.SerieYear == truck.SerieYear &&
                a.FabricationYer == truck.FabricationYer &&
                a.SeriesEnum == truck.SeriesEnum)), Times.Once);
        _mockRepository.Verify(x => x.CommitAsync(), Times.Once);
    }


    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldNotAcceptToUpdateTruckWithAnInvalidSeriesYear()
    {
        // Arrange
        var truck = new Truck(SeriesEnum.FH, _faker.Date.Past(1).Year);

        // Act
        await _truckService.UpdateAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Truck>()), Times.Never);
    }


    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldUpdateTruckSuccessfully()
    {
        // Arrange
        var truck = new Truck(SeriesEnum.FH, _faker.Date.Recent().Year);

        // Act
        await _truckService.UpdateAsync(truck).ConfigureAwait(false);

        // Assert
        _mockRepository.Verify(x =>
            x.UpdateAsync(It.Is<Truck>(a =>
                a.SerieYear == truck.SerieYear &&
                a.FabricationYer == truck.FabricationYer &&
                a.SeriesEnum == truck.SeriesEnum)), Times.Once);
        _mockRepository.Verify(x => x.CommitAsync(), Times.Once);
    }
}