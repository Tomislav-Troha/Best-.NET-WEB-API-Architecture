using BestArchitecture.Api.Controllers;
using BestArchitecture.Domain.Entities;
using BestArchitecture.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BestArchitecture.UnitTests;

public class CustomerControllerTests
{

    private readonly Mock<ICustomerRepository> _mockCustomerRepo;
    private readonly Mock<IOrderRepository> _mockOrdersRepo;
    private readonly CustomersController _customerController;

    public CustomerControllerTests()
    {
        _mockCustomerRepo = new Mock<ICustomerRepository>();
        _mockOrdersRepo = new Mock<IOrderRepository>();

        _customerController = new CustomersController(
            _mockCustomerRepo.Object,
            _mockOrdersRepo.Object);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        //Arrange
        _mockCustomerRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

        //Act
        var result = await _customerController.Get(1);

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
