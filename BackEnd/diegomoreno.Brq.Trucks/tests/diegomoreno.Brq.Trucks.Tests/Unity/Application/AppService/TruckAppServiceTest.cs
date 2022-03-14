using AutoMapper;
using Bogus;
using diegomoreno.Brq.Application.AppService;
using diegomoreno.Brq.Application.ViewModels.Trucks;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.domain.Interfaces.Contexts.Uow;
using diegomoreno.Brq.domain.Interfaces.Domain.Services;
using diegomoreno.Brq.domain.Interfaces.Repositories;
using diegomoreno.Brq.Trucks.Tests.Shared;
using DomainValidation.Validation;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diegomoreno.Brq.Application.ViewModels.Trucks.Response;
using Xunit;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace diegomoreno.Brq.Trucks.Tests.Unity.Application.AppService;

public class TruckAppServiceTest
{
    private readonly Mock<ITruckRepository> _truckRepository;
    private readonly Mock<ISeriesRepository> _seriesRepository;
    private readonly Mock<ITruckService> _truckService;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly TruckAppService _appService;
    private readonly Faker _faker;

    public TruckAppServiceTest()
    {
        var mocker = new AutoMocker();

        _faker = new Faker();
        _truckRepository = mocker.GetMock<ITruckRepository>();
        _seriesRepository = mocker.GetMock<ISeriesRepository>();
        _truckService = mocker.GetMock<ITruckService>();
        _mapper = mocker.GetMock<IMapper>();
        _uow = mocker.GetMock<IUnitOfWork>();

        _appService = mocker.CreateInstance<TruckAppService>();
    }

    [Fact]
    [Trait("Truck", "Get")]
    public async Task ShouldGetTruckById()
    {
        // Arrange
        var id = Guid.NewGuid();
        var truck = TruckBuilder.Novo().Build();

        var truckViewModel = new GetTruckResponseViewModel
        {
            SerieYear = truck.SerieYear,
            Series = truck.Series.Name,
            FabricationYear = truck.FabricationYear,
            Id = truck.Id
        };

        _truckRepository
            .Setup(t => t.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(truck);
        _mapper.Setup(m => m.Map<GetTruckResponseViewModel>(It.IsAny<Truck>())).Returns(truckViewModel);

        // Act
        var response = await _appService.GetAsync(id).ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(truckViewModel);
    }

    [Fact]
    [Trait("Truck", "Get")]
    public async Task ShouldGetAllTruck()
    {
        // Arrange
        var truck = TruckBuilder.Novo().Build();
        var trucks = new List<Truck> {truck};

        var truckViewModel = new GetTruckResponseViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            Series = "FH",
            FabricationYear = truck.FabricationYear,
            Id = truck.Id
        };
        var trucksViewModel = new List<GetTruckResponseViewModel> { truckViewModel };

        _truckRepository
            .Setup(t => t.GetAllAsync())
            .ReturnsAsync(trucks);
        _mapper.Setup(m => m.Map<IEnumerable<GetTruckResponseViewModel>>(It.IsAny<IEnumerable<Truck>>())).Returns(trucksViewModel);

        // Act
        var response = await _appService.GetAllAsync().ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(trucksViewModel);
    }

    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldNotAcceptToAddInvalidTruck()
    {
        // Arrange
        var validationResult = new ValidationResult();
        validationResult.Add(new ValidationError(_faker.Random.AlphaNumeric(20)));
        var truck = 
            TruckBuilder
                .Novo()
                .WithValidationResult(validationResult)
                .Build();

        var request = new AddTruckRequestViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
        };

        var truckViewModel = new TruckViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            FabricationYear = truck.FabricationYear,
            ValidationResult = truck.ValidationResult,
            Id = truck.Id
        };

        _truckService
            .Setup(t => t.AddAsync(It.IsAny<Truck>()))
            .ReturnsAsync(truck);
        _mapper.Setup(m => m.Map<Truck>(It.IsAny<TruckViewModel>())).Returns(truck);
        _mapper.Setup(m => m.Map<TruckViewModel>(It.IsAny<AddTruckRequestViewModel>())).Returns(truckViewModel);

        // Act
        var response = await _appService.AddAsync(request).ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(truckViewModel);
        _uow.Verify(u => u.CommitAsync(), Times.Never);
    }
    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldReturnValidationErrorWhenAddingTruckAndCommitFails()
    {
        // Arrange
        const string expectedMessage = "An error occurred while saving the data in the database.";
        var truck = TruckBuilder.Novo().Build();
        var series = SeriesBuilder.Novo().Build();
        
        var request = new AddTruckRequestViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
        };

        var truckViewModel = new TruckViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            FabricationYear = truck.FabricationYear,
            ValidationResult = truck.ValidationResult,
            Id = truck.Id
        };

        _truckService
            .Setup(t => t.AddAsync(It.IsAny<Truck>()))
            .ReturnsAsync(truck);
        _seriesRepository
            .Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(series);
        _mapper.Setup(m => m.Map<TruckViewModel>(It.IsAny<AddTruckRequestViewModel>())).Returns(truckViewModel);
        _uow.Setup(u => u.CommitAsync()).ReturnsAsync(false);

        // Act
        var response = await _appService.AddAsync(request).ConfigureAwait(false);

        // Assert
        Assert.False(response.ValidationResult.IsValid);
        Assert.Equal(expectedMessage, response.ValidationResult.Erros.ToArray()[0].Message);
    }

    [Fact]
    [Trait("Truck", "Add")]
    public async Task ShouldAddValidTruck()
    {
        // Arrange
        var truck = TruckBuilder.Novo().Build();
        var series = SeriesBuilder.Novo().Build();

        var request = new AddTruckRequestViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
        };

        var truckViewModel = new TruckViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            FabricationYear = truck.FabricationYear,
            ValidationResult = truck.ValidationResult,
            Id = truck.Id
        };

        _truckService
            .Setup(t => t.AddAsync(It.IsAny<Truck>()))
            .ReturnsAsync(truck);
        _seriesRepository
            .Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(series);
        _mapper.Setup(m => m.Map<TruckViewModel>(It.IsAny<AddTruckRequestViewModel>())).Returns(truckViewModel);
        _uow.Setup(u => u.CommitAsync()).ReturnsAsync(true);

        // Act
        var response = await _appService.AddAsync(request).ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(truckViewModel);
        _truckService.Verify(t => t.AddAsync(It.Is<Truck>(x =>
            x.SerieYear == truck.SerieYear &&
            x.FabricationYear == truck.FabricationYear &&
            x.IdSeries == series.Id)), Times.Once);
        _uow.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldNotAcceptToUpdateInvalidTruck()
    {
        // Arrange
        var validationResult = new ValidationResult();
        validationResult.Add(new ValidationError(_faker.Random.AlphaNumeric(20)));
        var truck =
            TruckBuilder
                .Novo()
                .WithValidationResult(validationResult)
                .Build();
        

        var request = new UpdateTruckRequestViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            Id = truck.Id,
            FabricationYer = truck.FabricationYear
        };

        var truckViewModel = new TruckViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            FabricationYear = truck.FabricationYear,
            ValidationResult = truck.ValidationResult,
            Id = truck.Id
        };

        _truckService
            .Setup(t => t.UpdateAsync(It.IsAny<Truck>()))
            .ReturnsAsync(truck);
        _truckRepository
            .Setup(t => t.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(truck);
        _mapper.Setup(m => m.Map<TruckViewModel>(It.IsAny<UpdateTruckRequestViewModel>())).Returns(truckViewModel);

        // Act
        var response = await _appService.UpdateAsync(request, Guid.NewGuid()).ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(truckViewModel);
        _uow.Verify(u => u.CommitAsync(), Times.Never);
    }
    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldReturnValidationErrorWhenUpdatingTruckAndCommitFails()
    {
        // Arrange
        const string expectedMessage = "An error occurred while saving the data in the database.";
        var truck = TruckBuilder.Novo().Build();
        var series = SeriesBuilder.Novo().Build();

        var request = new UpdateTruckRequestViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            Id = truck.Id,
            FabricationYer = truck.FabricationYear
        };

        var truckViewModel = new TruckViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            FabricationYear = truck.FabricationYear,
            ValidationResult = truck.ValidationResult,
            Id = truck.Id
        };

        _truckService
            .Setup(t => t.UpdateAsync(It.IsAny<Truck>()))
            .ReturnsAsync(truck);
        _seriesRepository
            .Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(series);
        _truckRepository
            .Setup(t => t.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(truck);
        _mapper.Setup(m => m.Map<TruckViewModel>(It.IsAny<UpdateTruckRequestViewModel>())).Returns(truckViewModel);
        _uow.Setup(u => u.CommitAsync()).ReturnsAsync(false);

        // Act
        var response = await _appService.UpdateAsync(request, request.Id).ConfigureAwait(false);

        // Assert
        Assert.False(response.ValidationResult.IsValid);
        Assert.Equal(expectedMessage, response.ValidationResult.Erros.ToArray()[0].Message);
    }

    [Fact]
    [Trait("Truck", "Update")]
    public async Task ShouldUpdateValidTruck()
    {
        // Arrange
        var truck = TruckBuilder.Novo().Build();
        var series = SeriesBuilder.Novo().Build();

        var request = new UpdateTruckRequestViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            Id = truck.Id,
            FabricationYer = truck.FabricationYear
        };

        var truckViewModel = new TruckViewModel
        {
            SerieYear = truck.SerieYear,
            IdSeries = truck.IdSeries,
            FabricationYear = truck.FabricationYear,
            ValidationResult = truck.ValidationResult,
            Id = truck.Id
        };

        _truckService
            .Setup(t => t.UpdateAsync(It.IsAny<Truck>()))
            .ReturnsAsync(truck);
        _seriesRepository
            .Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(series);
        _truckRepository
            .Setup(t => t.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(truck);
        _mapper.Setup(m => m.Map<TruckViewModel>(It.IsAny<UpdateTruckRequestViewModel>())).Returns(truckViewModel);
        _uow.Setup(u => u.CommitAsync()).ReturnsAsync(true);

        // Act
        var response = await _appService.UpdateAsync(request, request.Id).ConfigureAwait(false);

        // Assert
        response.Should().BeEquivalentTo(truckViewModel);
        _truckService.Verify(t => t.UpdateAsync(It.Is<Truck>(x =>
            x.SerieYear == truck.SerieYear &&
            x.FabricationYear == truck.FabricationYear &&
            x.IdSeries == series.Id)), Times.Once);
        _uow.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    [Trait("Truck", "Delete")]
    public async Task ShouldDeleteTruck()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _appService.DeleteAsync(id).ConfigureAwait(false);

        // Assert
        _truckService.Verify(t => t.DeleteAsync(id), Times.Once);
    }
}