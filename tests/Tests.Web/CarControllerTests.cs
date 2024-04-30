using Xunit;
using Web;
using Moq;
using Web.Endpoints;
using MediatR;
using AutoMapper;
using Application.Cars.Queries;
using Application.DTOs.Car;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

namespace Tests.Web
{
    public class CarControllerTests
    {
        private CarController _controller;
        private Mock<IMediator> _mediatorMock;
        private Mock<IMapper> _mapperMock;

        public CarControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CarController(_mapperMock.Object, _mediatorMock.Object, null);
        }

        //[Fact]
        //public async Task GetCars_ReturnsOkResult_WithListOfCars()
        //{
        //    // Arrange
        //    var cars = new List<CarDTO>() { new CarDTO(), new CarDTO() };
        //    _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCars>(), CancellationToken.None)).Returns(Task.FromResult<IEnumerable<Car>>(cars));
        //    _mapperMock.Setup(x => x.Map<IEnumerable<CarDTO>>(It.IsAny<IEnumerable<CarDTO>>())).Returns(cars);

        //    // Act
        //    var result = await _controller.GetCars();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var carDTOs = Assert.IsAssignableFrom<IEnumerable<CarDTO>>(okResult.Value);
        //    Assert.Equal(cars.Count, carDTOs.Count);
        //}
    }
}