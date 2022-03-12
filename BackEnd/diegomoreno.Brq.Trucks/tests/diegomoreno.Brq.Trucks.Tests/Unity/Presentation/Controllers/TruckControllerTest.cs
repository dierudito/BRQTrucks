using Bogus;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels;
using diegomoreno.Brq.bff.Controllers.v1;
using diegomoreno.Brq.domain.Enums;
using diegomoreno.Brq.Trucks.Tests.Shared;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using Xunit;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Presentation.Controllers;

public class TruckControllerTest
{
    private readonly TruckController _truckController;
    private readonly Mock<ITruckAppService> _truckAppService;
    private readonly Faker _faker;

    public TruckControllerTest()
    {
        _faker = new Faker();
        var mocker = new AutoMocker();

        _truckAppService = mocker.GetMock<ITruckAppService>();
        _truckController = mocker.CreateInstance<TruckController>();
    }

    [Fact]
    [Trait("Truck", "Get")]
    public async Task ShouldReturnNotFoundWhenNotFindingTheTruck()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int expectedStatusCode = 404;

        _truckAppService.Setup(t => t.GetAsync(id)).ReturnsAsync((TruckViewModel)null);

        // Act
        var response = await _truckController.Get(id).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
    }

    [Fact]
    [Trait("Truck", "Get")]
    public async Task ShouldGetTruckById()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int expectedStatusCode = 200;

        var truckViewModel = new TruckViewModel
        {
            SerieYear = _faker.Random.Number(2000,3000),
            SeriesEnum = SeriesEnum.FH,
            FabricationYer = _faker.Random.Number(2000, 3000),
            Id = id
        };

        _truckAppService.Setup(t => t.GetAsync(id)).ReturnsAsync(truckViewModel);

        // Act
        var response = await _truckController.Get(id).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);
        var valueResponse = response.GetType().GetProperty("Value")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        Assert.Equal(truckViewModel, valueResponse);
    }

    [Fact]
    [Trait("Truck", "Get")]
    public async Task ShouldGetAllTruck()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int expectedStatusCode = 200;

        var truckViewModel = new TruckViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            SeriesEnum = SeriesEnum.FH,
            FabricationYer = _faker.Random.Number(2000, 3000),
            Id = Guid.NewGuid()
        };

        var trucksViewModel = new List<TruckViewModel>{ truckViewModel};

        _truckAppService.Setup(t => t.GetAllAsync()).ReturnsAsync(trucksViewModel);

        // Act
        var response = await _truckController.GetAll().ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);
        var valueResponse = response.GetType().GetProperty("Value")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        Assert.Equal(trucksViewModel, valueResponse);
    }

    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldAddTruck()
    {
        // Arrange
        const int expectedStatusCode = 200;
        var truckViewModel = new AddTruckRequestViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            SeriesEnum = SeriesEnum.FH
        };


        // Act
        var response = await _truckController.AddAsync(truckViewModel).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        _truckAppService.Verify(t => t.AddAsync(It.Is<AddTruckRequestViewModel>(x =>
                x.SerieYear == truckViewModel.SerieYear &&
                x.SeriesEnum == truckViewModel.SeriesEnum)),
            Times.Once());
    }

    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldUpdateTruck()
    {
        // Arrange
        const int expectedStatusCode = 200;
        var truckViewModel = new UpdateTruckRequestViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            SeriesEnum = SeriesEnum.FH,
            Id = Guid.NewGuid(),
            FabricationYer = _faker.Random.Number(2000, 3000)
        };


        // Act
        var response = await _truckController.UpdateAsync(truckViewModel).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        _truckAppService.Verify(t => t.UpdateAsync(It.Is<UpdateTruckRequestViewModel>(x =>
                x.SerieYear == truckViewModel.SerieYear &&
                x.SeriesEnum == truckViewModel.SeriesEnum &&
                x.Id == truckViewModel.Id)),
            Times.Once());
    }

    [Fact]
    [Trait("Truck", "Delete")]
    public async Task ShouldDeleteTruck()
    {
        // Arrange
        const int expectedStatusCode = 200;
        var id = Guid.NewGuid();


        // Act
        var response = await _truckController.DeleteAsync(id).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        _truckAppService.Verify(t => t.DeleteAsync(id),
            Times.Once());
    }
}