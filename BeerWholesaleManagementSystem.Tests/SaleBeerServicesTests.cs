using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Services.Services;
using Moq;
using Xunit;

namespace BeerWholesaleManagementSystem.Tests;

public class SaleBeerServicesTests
{
    [Fact]
    [Trait("categories", "Create Sale Beer")]
    public async Task Should_CreateSaleBeer_WhenValidData_ReturnsSaleBeer()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var beerId = 1;
        var wholesalerId = 2;
        var quantity = 10;

        var saleBeerRepositoryMock = new Mock<ISaleBeerRepositories>();
        var beerRepositoryMock = new Mock<IBeerRepository>();
        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();

        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerId, cancellationToken))
                         .ReturnsAsync(new Beer { BeerId = beerId, Name = "Test Beer" });

        wholesalerRepositoryMock.Setup(repo => repo.GetByIdAsync(wholesalerId, cancellationToken))
                               .ReturnsAsync(new Wholesaler { WholesalerId = wholesalerId, Name = "Test Wholesaler" });

        var saleBeerServices = new SaleBeerService(saleBeerRepositoryMock.Object, beerRepositoryMock.Object, wholesalerRepositoryMock.Object);

        // Act
        var newSaleBeer = new SaleBeer
        {
            BeerId = beerId,
            WholesalerId = wholesalerId,
            Quantity = quantity
        };

        var result = await saleBeerServices.CreateSaleBeer(newSaleBeer, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(beerId, result.BeerId);
        Assert.Equal(wholesalerId, result.WholesalerId);
        Assert.Equal(quantity, result.Quantity);
        Assert.True(result.DateSale > DateTime.MinValue);

        saleBeerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<SaleBeer>(), cancellationToken), Times.Once);
    }

    [Fact]
    [Trait("categories", "Create Sale Beer")]
    public async Task Should_ThrowInvalidOperationException_WhenBeerDoesNotExist()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var beerId = 1;
        var wholesalerId = 2;
        var quantity = 10;

        var saleBeerRepositoryMock = new Mock<ISaleBeerRepositories>();
        var beerRepositoriesMock = new Mock<IBeerRepository>();
        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();

        beerRepositoriesMock.Setup(repo => repo.GetByIdAsync(beerId, cancellationToken))
                         .ReturnsAsync((Beer)null!); 

        var saleBeerServices = new SaleBeerService(saleBeerRepositoryMock.Object, beerRepositoriesMock.Object, wholesalerRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            var newSaleBeer = new SaleBeer
            {
                BeerId = beerId,
                WholesalerId = wholesalerId,
                Quantity = quantity
            };

            return saleBeerServices.CreateSaleBeer(newSaleBeer, cancellationToken);
        });

        saleBeerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<SaleBeer>(), cancellationToken), Times.Never);
    }

    [Fact]
    [Trait("categories", "Create Sale Beer")]
    public async Task Should_ThrowInvalidOperationException_WhenWholesalerDoesNotExist()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var beerId = 1;
        var wholesalerId = 2;
        var quantity = 10;

        var saleBeerRepositoryMock = new Mock<ISaleBeerRepositories>();
        var beerRepositoryMock = new Mock<IBeerRepository>();
        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();

        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerId, cancellationToken))
                         .ReturnsAsync(new Beer { BeerId = beerId, Name = "Test Beer" });

        wholesalerRepositoryMock.Setup(repo => repo.GetByIdAsync(wholesalerId, cancellationToken))
                               .ReturnsAsync((Wholesaler)null!); 

        var saleBeerServices = new SaleBeerService(saleBeerRepositoryMock.Object, beerRepositoryMock.Object, wholesalerRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            var newSaleBeer = new SaleBeer
            {
                BeerId = beerId,
                WholesalerId = wholesalerId,
                Quantity = quantity
            };

            return saleBeerServices.CreateSaleBeer(newSaleBeer, cancellationToken);
        });
        saleBeerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<SaleBeer>(), cancellationToken), Times.Never);
    }
}

