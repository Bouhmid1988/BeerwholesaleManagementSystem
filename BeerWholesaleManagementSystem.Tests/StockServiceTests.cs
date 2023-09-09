using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Services.Services;
using Moq;
using Xunit;

namespace BeerWholesaleManagementSystem.Test.Services;

public class StockServiceTests
{
    [Fact]
    [Trait("categories", "Stock")]
    public async Task GetStockById_WhenStockExists_ReturnsStock()
    {
        // Arrange
        var stockId = 1;
        var cancellationToken = CancellationToken.None;
        var expectedStock = new Stock { StockId = stockId, QuantityStock = 10 };

        var stockRepositoryMock = new Mock<IStockRepositories>();
        stockRepositoryMock.Setup(repo => repo.GetByIdAsync(stockId, cancellationToken))
                           .ReturnsAsync(expectedStock);

        var stockService = new StockService(stockRepositoryMock.Object);

        // Act
        var result = await stockService.GetStockById(stockId, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(stockId, result.StockId);
        Assert.Equal(expectedStock.QuantityStock, result.QuantityStock);
        stockRepositoryMock.Verify(repo => repo.GetByIdAsync(stockId, cancellationToken), Times.Once);
    }
    [Fact]
    [Trait("categories", "Stock")]
    public async Task GetStockById_WhenStockDoesNotExist_ReturnsNull()
    {
        // Arrange
        var stockId = 1;
        var cancellationToken = CancellationToken.None;
        var stockRepositoryMock = new Mock<IStockRepositories>();

        stockRepositoryMock.Setup(repo => repo.GetByIdAsync(stockId, cancellationToken))
                           .ReturnsAsync((Stock)null!);
        var stockService = new StockService(stockRepositoryMock.Object);

        // Act
        var result = await stockService.GetStockById(stockId, cancellationToken);

        // Assert
        Assert.Null(result);
        stockRepositoryMock.Verify(repo => repo.GetByIdAsync(stockId, cancellationToken), Times.Once);
    }

    [Fact]
    [Trait("categories", "Stock")]
    public async Task UpdateStockQuantity_WhenStockExists_ReturnsUpdatedStock()
    {
        // Arrange
        var stockId = 1;
        var newQuantity = 20;
        var cancellationToken = CancellationToken.None;
        var existingStock = new Stock { StockId = stockId, QuantityStock = 10 };

        var stockRepositoryMock = new Mock<IStockRepositories>();
        stockRepositoryMock.Setup(repo => repo.GetByIdAsync(stockId, cancellationToken))
                           .ReturnsAsync(existingStock);
        var stockService = new StockService(stockRepositoryMock.Object);

        // Act
        var result = await stockService.UpdateStockQuantity(stockId, newQuantity, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(stockId, result.StockId);
        Assert.Equal(newQuantity, result.QuantityStock);
        stockRepositoryMock.Verify(repo => repo.GetByIdAsync(stockId, cancellationToken), Times.Once);
        stockRepositoryMock.Verify(repo => repo.UpdateAsync(existingStock, cancellationToken), Times.Once);
    }
    [Fact]
    [Trait("categories", "Stock")]
    public async Task UpdateStockQuantity_QuandLeStockNExistePas_ReturnsException()
    {
        // Arrange
        var stockId = 1;
        var newQuantity = 20;
        var cancellationToken = CancellationToken.None;
        var stockRepositoryMock = new Mock<IStockRepositories>();

        stockRepositoryMock.Setup(repo => repo.GetByIdAsync(stockId, cancellationToken))
                           .ReturnsAsync((Stock)null!);
        var stockService = new StockService(stockRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => stockService.UpdateStockQuantity(stockId, newQuantity, cancellationToken));
        stockRepositoryMock.Verify(repo => repo.GetByIdAsync(stockId, cancellationToken), Times.Once);
    }


}
