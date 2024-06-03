using Moq;
using Web.Endpoints;
using MediatR;
using AutoMapper;
using Application.Cars.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Application.DTOs.Car;
using FluentAssertions;

namespace Tests.Web
{
    public class CarControllerTests
    {
        private readonly CarController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IWebHostEnvironment> _hostingEnvironmentMock;

        public CarControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            _controller = new CarController(_mapperMock.Object, _mediatorMock.Object, _hostingEnvironmentMock.Object);
        }

        [Fact]
        public async Task GetCars_ReturnsOkResult_WithListOfCars()
        {
            // Arrange
            var expectedCars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    ModelId = 3,
                    GenerationId = 1,
                    Year = 2018,
                    Price = 18500.50m,
                    Mileage = 85000,
                    VIN = "JH4KB2H13EK317822",
                    BodyTypeId = 2,
                    GearBoxTypeId = 1,
                    DriveTrainId = 2,
                    FuelTypeId = 1,
                    Description = "This spacious SUV is perfect for families. Needs minor repairs."
                },
                new Car
                {
                    Id = 2,
                    ModelId = 4,
                    GenerationId = 2,
                    Year = 2020,
                    Price = 25000.00m,
                    Mileage = 50000,
                    VIN = "1HGCM82633A123456",
                    BodyTypeId = 1,
                    GearBoxTypeId = 2,
                    DriveTrainId = 1,
                    FuelTypeId = 2,
                    Description = "Sporty sedan with excellent performance."
                }
            };

            var expectedCarDTOs = expectedCars.Select(c => new CarDTO
            {
                Id = c.Id,
                MakeName = "Make", 
                ModelName = "Model", 
                GenerationName = "Generation",
                ModificationName = "Modification", 
                VIN = c.VIN,
                BodyTypeName = "BodyType", 
                GearBoxTypeName = "GearBox", 
                DriveTrainName = "DriveTrain", 
                TechnicalConditionName = "Condition", 
                FuelTypeName = "FuelType",
                Year = c.Year,
                Price = c.Price,
                Mileage = c.Mileage,
                Description = c.Description,
                FirstName = "First", 
                LastName = "Last", 
                UserEmail = "user@example.com", 
                UserPhoneNumber = "123-456-7890", 
                ImagesPath = new List<string> { "path/to/image.jpg" },
                IsAdvertisementApproved = true,
                DateCreated = DateTime.UtcNow
            }).ToList();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCars>(), default)).ReturnsAsync(expectedCars);
            _mapperMock.Setup(m => m.Map<IEnumerable<CarDTO>>(It.IsAny<IEnumerable<Car>>())).Returns(expectedCarDTOs);

            // Act
            var result = await _controller.GetCars();

            // Assert
            var okResult = result as Microsoft.AspNetCore.Http.HttpResults.Ok<List<CarDTO>>;
            okResult.Should().NotBeNull();

            var carDTOs = okResult.Value;
            carDTOs.Should().HaveCount(2);
            carDTOs[0].Id.Should().Be(expectedCars[0].Id);
            carDTOs[0].ModelName.Should().Be("Model");
            carDTOs[1].Id.Should().Be(expectedCars[1].Id);
            carDTOs[1].ModelName.Should().Be("Model");
        }
    }
}