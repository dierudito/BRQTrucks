using Bogus;
using diegomoreno.Brq.Application.Interfaces;
using diegomoreno.Brq.Application.ViewModels.Series;
using diegomoreno.Brq.bff.Controllers.v1;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Presentation.Controllers;

public class SeriesControllerTest
{
    private readonly SeriesController _seriesController;
    private readonly Mock<ISeriesAppService> _seriesAppService;
    private readonly Faker _faker;

    public SeriesControllerTest()
    {
        _faker = new Faker();
        var mocker = new AutoMocker();

        _seriesAppService = mocker.GetMock<ISeriesAppService>();
        _seriesController = mocker.CreateInstance<SeriesController>();
    }

    [Fact]
    [Trait("Series", "Get")]
    public async Task ShouldReturnNotFoundWhenNotFindingTheSeries()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int expectedStatusCode = 404;

        _seriesAppService.Setup(t => t.GetAsync(id)).ReturnsAsync((SeriesViewModel)null);

        // Act
        var response = await _seriesController.Get(id).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
    }

    [Fact]
    [Trait("Series", "Get")]
    public async Task ShouldGetSeriesById()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int expectedStatusCode = 200;

        var seriesViewModel = new SeriesViewModel
        {
            Name = "FM",
            Id = id
        };

        _seriesAppService.Setup(t => t.GetAsync(id)).ReturnsAsync(seriesViewModel);

        // Act
        var response = await _seriesController.Get(id).ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);
        var valueResponse = response.GetType().GetProperty("Value")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        Assert.Equal(seriesViewModel, valueResponse);
    }

    [Fact]
    [Trait("Series", "Get")]
    public async Task ShouldGetAllSeries()
    {
        // Arrange
        var id = Guid.NewGuid();
        const int expectedStatusCode = 200;

        var seriesViewModel = new SeriesViewModel
        {
            Name = "FM",
            Id = id
        };

        var listSeriesViewModel = new List<SeriesViewModel> { seriesViewModel };

        _seriesAppService.Setup(t => t.GetAllAsync()).ReturnsAsync(listSeriesViewModel);

        // Act
        var response = await _seriesController.GetAll().ConfigureAwait(false);
        var value = response.GetType().GetProperty("StatusCode")?.GetValue(response);
        var valueResponse = response.GetType().GetProperty("Value")?.GetValue(response);

        // Assert
        Assert.Equal(expectedStatusCode, value);
        Assert.Equal(listSeriesViewModel, valueResponse);
    }
}