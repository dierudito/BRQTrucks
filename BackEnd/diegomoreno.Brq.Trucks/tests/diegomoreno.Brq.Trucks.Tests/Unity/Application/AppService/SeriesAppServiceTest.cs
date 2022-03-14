using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using diegomoreno.Brq.Application.AppService;
using diegomoreno.Brq.Application.ViewModels.Series;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.Trucks.Tests.Shared;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Application.AppService;

public class SeriesAppServiceTest
{
    private readonly Mock<ISeriesRepository> _seriesRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly SeriesAppService _appService;
    private readonly Faker _faker;

    public SeriesAppServiceTest()
    {
        var mocker = new AutoMocker();

        _faker = new Faker();
        _seriesRepository = mocker.GetMock<ISeriesRepository>();
        _mapper = mocker.GetMock<IMapper>();
        _appService = mocker.CreateInstance<SeriesAppService>();
    }

    [Fact]
    [Trait("Series", "Get")]
    public async Task ShouldGetSeriesById()
    {
        // Arrange
        var id = Guid.NewGuid();
        var series = SeriesBuilder.Novo().Build();

        var seriesViewModel = new SeriesViewModel
        {
            Name = series.Name,
            Id = series.Id
        };

        _seriesRepository
            .Setup(t => t.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(series);
        _mapper.Setup(m => m.Map<SeriesViewModel>(It.IsAny<Series>())).Returns(seriesViewModel);

        // Act
        var response = await _appService.GetAsync(id).ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(seriesViewModel);
    }
    [Fact]
    [Trait("Series", "Get")]
    public async Task ShouldGetAllSeries()
    {
        // Arrange
        var series = SeriesBuilder.Novo().Build();
        var listSeries = new List<Series> { series };

        var seriesViewModel = new SeriesViewModel
        {
            Name = series.Name,
            Id = series.Id
        };
        var listSeriesViewModel = new List<SeriesViewModel> { seriesViewModel };

        _seriesRepository
            .Setup(t => t.GetAllAsync())
            .ReturnsAsync(listSeries);
        _mapper.Setup(m => m.Map<IEnumerable<SeriesViewModel>>(It.IsAny<IEnumerable<Series>>())).Returns(listSeriesViewModel);

        // Act
        var response = await _appService.GetAllAsync().ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(listSeriesViewModel);
    }
}