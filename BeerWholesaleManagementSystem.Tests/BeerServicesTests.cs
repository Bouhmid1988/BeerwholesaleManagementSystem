using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Services.Services;
using Moq;
using Xunit;

namespace BeerWholesaleManagementSystem.Tests;

public class BeerServicesTests
{
    [Fact]
    [Trait("categories", "New beer")]
    public async Task Should_CreateBeer_WhenBeerDoesNotExist_ReturnsNewBeer()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var newBeer = new Beer { BeerId = 1, Name = "Bière 1", AlcoholContent = 5.0m, Price = 3.99m, BreweryId = 1 };
        var beerRepositoryMock = new Mock<IBeerRepository>();

        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(newBeer.BeerId, cancellationToken))
                         .ReturnsAsync((Beer)null!);

        beerRepositoryMock.Setup(repo => repo.AddAsync(newBeer, cancellationToken))
                         .ReturnsAsync(newBeer);

        var beerService = new BeerService(beerRepositoryMock.Object);

        // Act
        var result = await beerService.CreateBeer(newBeer, cancellationToken);

        // Assert
        beerRepositoryMock.Verify(repo => repo.GetByIdAsync(newBeer.BeerId, cancellationToken), Times.Once);
        beerRepositoryMock.Verify(repo => repo.AddAsync(newBeer, cancellationToken), Times.Once);
        Assert.Equal(newBeer, result);
    }

    [Fact]
    [Trait("categories", "New beer")]
    public async Task Should_CreateBeer_WhenBeerExists_ThrowsInvalidOperationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var newBeer = new Beer { BeerId = 1, Name = "Bière 1", AlcoholContent = 5.0m, Price = 3.99m, BreweryId = 1 };
        var existingBeer =
            new Beer
            {
                BeerId = newBeer.BeerId,
                Name = newBeer.Name,
                AlcoholContent = newBeer.AlcoholContent,
                Price = newBeer.Price,
                BreweryId = newBeer.BreweryId
            };
        var beerRepositoriesMock = new Mock<IBeerRepository>();
        beerRepositoriesMock.Setup(repo => repo.GetByIdAsync(newBeer.BeerId, cancellationToken))
                         .ReturnsAsync(existingBeer);

        var beerService = new BeerService(beerRepositoriesMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => beerService.CreateBeer(newBeer, cancellationToken));
        beerRepositoriesMock.Verify(repo => repo.GetByIdAsync(newBeer.BeerId, cancellationToken), Times.Once);

    }

    [Fact]
    [Trait("categories", "Delete beer")]
    public async Task Should_DeleteBeer_WhenBeerExists_RemovesBeer()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var beerIdToDelete = 1;
        var existingBeer = new Beer { BeerId = 1, Name = "Bière 1", AlcoholContent = 5.0m, Price = 3.99m, BreweryId = 1 };

        var beerRepositoryMock = new Mock<IBeerRepository>();
        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerIdToDelete, cancellationToken))
                         .ReturnsAsync(existingBeer);

        var beerService = new BeerService(beerRepositoryMock.Object);

        // Act
        await beerService.DeleteBeer(existingBeer, cancellationToken);

        // Assert
        beerRepositoryMock.Verify(repo => repo.RemoveAsync(existingBeer, cancellationToken), Times.Once);
    }

    [Fact]
    [Trait("categories", "Delete beer")]
    public async Task Should_DeleteBeer_WhenBeerDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var beerIdToDelete = 1;

        var beerRepositoryMock = new Mock<IBeerRepository>();
        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerIdToDelete, cancellationToken))
                         .Returns(Task.FromResult<Beer>(null!));
        var beerService = new BeerService(beerRepositoryMock.Object);

        // Act & Assert
        async Task DeleteBeer() => await beerService.DeleteBeer(new Beer { BeerId = 1, Name = "Bière 1", AlcoholContent = 5.0m, Price = 3.99m, BreweryId = 1 }, cancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(DeleteBeer);
        beerRepositoryMock.Verify(repo => repo.GetByIdAsync(beerIdToDelete, cancellationToken), Times.Once);
    }

    [Fact]
    [Trait("categories", "List of beer by brewery")]
    public async Task Should_GetBeersByBrewery_WhenValidId_ReturnsListOfBeers()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var breweryId = 1;
        var expectedBeers = new List<Beer>
            {
                new Beer { BeerId = 1, Name = "Bière 1", AlcoholContent = 5.0m, Price = 3.99m, BreweryId = 1 },
                new Beer { BeerId = 2, Name = "Bière 2", AlcoholContent = 7.0m, Price = 4.99m, BreweryId = 1 }
            };

        var beerRepositoryMock = new Mock<IBeerRepository>();
        beerRepositoryMock.Setup(repo => repo.GetBeersByBrewery(breweryId, cancellationToken))
                         .ReturnsAsync(expectedBeers);

        var breweryService = new BeerService(beerRepositoryMock.Object);

        // Act
        var result = await breweryService.GetBeersByBrewery(breweryId, cancellationToken);

        // Assert
        Assert.Equal(expectedBeers, result.ToList());
        beerRepositoryMock.Verify(repo => repo.GetBeersByBrewery(breweryId, cancellationToken), Times.Once);
    }

    [Fact]
    [Trait("categories", "List of beer by brewery")]
    public async Task Should_GetBeersByBrewery_WhenValidIdAndNoBeers_ThrowsInvalidOperationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var breweryId = 1;

        var beerRepositoryMock = new Mock<IBeerRepository>();
        beerRepositoryMock.Setup(repo => repo.GetBeersByBrewery(breweryId, cancellationToken))
                         .ReturnsAsync(new List<Beer>()); 

        var breweryService = new BeerService(beerRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            breweryService.GetBeersByBrewery(breweryId, cancellationToken));
        beerRepositoryMock.Verify(repo => repo.GetBeersByBrewery(breweryId, cancellationToken), Times.Once);
    }
}


