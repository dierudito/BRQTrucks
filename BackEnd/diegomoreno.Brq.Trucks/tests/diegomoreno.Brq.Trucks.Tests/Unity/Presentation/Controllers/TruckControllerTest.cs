using Bogus;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.bff.Controllers.v1;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diegomoreno.Brq.Application.ViewModels.Trucks.Response;
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

        _truckAppService.Setup(t => t.GetAsync(id)).ReturnsAsync((GetTruckResponseViewModel)null);

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

        var truckViewModel = new GetTruckResponseViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            Series = "FM",
            FabricationYear = _faker.Random.Number(2000, 3000),
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
        const int expectedStatusCode = 200;

        var truckViewModel = new GetTruckResponseViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            IdSeries = Guid.NewGuid(),
            Series = "FM",
            Id = Guid.NewGuid()
        };

        var trucksViewModel = new List<GetTruckResponseViewModel> { truckViewModel };

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
        var request = new AddTruckRequestViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            IdSeries = Guid.NewGuid()
        };

        var truckViewModel = new TruckViewModel
        {
            FabricationYear = _faker.Date.Recent().Year,
            Id = Guid.NewGuid(),
            IdSeries = request.IdSeries,
            SerieYear = request.SerieYear
        };

        _truckAppService.Setup(t => t.AddAsync(It.IsAny<AddTruckRequestViewModel>())).ReturnsAsync(truckViewModel);


        // Act
        var response = await _truckController.AddAsync(request).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        _truckAppService.Verify(t => t.AddAsync(It.Is<AddTruckRequestViewModel>(x =>
                x.SerieYear == request.SerieYear &&
                x.IdSeries == request.IdSeries)),
            Times.Once());
    }

    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldUpdateTruck()
    {
        // Arrange
        const int expectedStatusCode = 200;
        var request = new UpdateTruckRequestViewModel
        {
            SerieYear = _faker.Random.Number(2000, 3000),
            IdSeries = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            FabricationYer = _faker.Random.Number(2000, 3000)
        };

        var truckViewModel = new TruckViewModel
        {
            FabricationYear = request.FabricationYer,
            Id = request.Id,
            IdSeries = request.IdSeries,
            SerieYear = request.SerieYear
        };

        _truckAppService.Setup(t => t.UpdateAsync(It.IsAny<UpdateTruckRequestViewModel>(), It.IsAny<Guid>())).ReturnsAsync(truckViewModel);


        // Act
        var response = await _truckController.UpdateAsync(request, request.Id).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        _truckAppService.Verify(t => t.UpdateAsync(It.Is<UpdateTruckRequestViewModel>(x =>
                x.SerieYear == request.SerieYear &&
                x.IdSeries == request.IdSeries &&
                x.Id == request.Id), request.Id),
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