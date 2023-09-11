using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Services.Services;
using Moq;
using Xunit;

namespace BeerWholesaleManagementSystem.Tests;

public class WholesalerServiceTests
{
    [Fact]
    [Trait("categories", "Generate Quote")]

    public async Task GenerateQuote_WithValidData_ReturnsQuoteResponseDto()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var quoteRequest = new QuoteResquestDto
        {
            WholesalerId = 2,
            RequestDate = DateTime.UtcNow,
            QuoteBeerItemDto = new List<QuoteBeerItemRequestDto>
            {
                new QuoteBeerItemRequestDto { BeerId = 6, Quantity = 5 },
                new QuoteBeerItemRequestDto { BeerId = 4, Quantity = 3 }
            }
        };

        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
        var stockRepositoryMock = new Mock<IStockRepository>();
        var beerRepositoryMock = new Mock<IBeerRepository>();
        var quoteRequestRepositoryMock = new Mock<IQuoteRequestRepository>();
        var quoteRequestBeerDetailsRepositoryMock = new Mock<IQuoteRequestBeerDetailsRepository>();

        wholesalerRepositoryMock.Setup(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken))
            .ReturnsAsync(new Wholesaler { WholesalerId = quoteRequest.WholesalerId });

        stockRepositoryMock.Setup(repo => repo.GetStockByBeerAndWholesalerId(It.IsAny<int>(), It.IsAny<int>(), cancellationToken))
            .ReturnsAsync(new Stock { QuantityStock = 10 }); 

        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), cancellationToken))
            .ReturnsAsync(new Beer { Price = 5.0m }); 

        var wholesalerService = new WholesalerService(
            wholesalerRepositoryMock.Object,
            stockRepositoryMock.Object,
            beerRepositoryMock.Object,
            quoteRequestRepositoryMock.Object,
            quoteRequestBeerDetailsRepositoryMock.Object);

        // Act
        var result = await wholesalerService.GenerateQuote(quoteRequest, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(quoteRequest.WholesalerId, result.WholesalerId);
        Assert.Equal("in progress", result.Statuts); 

        // Verify repository method calls
        wholesalerRepositoryMock.Verify(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken), Times.Once);
        stockRepositoryMock.Verify(
            repo => repo.GetStockByBeerAndWholesalerId(It.IsAny<int>(), It.IsAny<int>(), cancellationToken),
            Times.Exactly(quoteRequest.QuoteBeerItemDto.Count));
        beerRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), cancellationToken), Times.Exactly(quoteRequest.QuoteBeerItemDto.Count));
        quoteRequestRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<QuoteRequest>(), cancellationToken), Times.Once);
        quoteRequestBeerDetailsRepositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<QuoteRequestBeerDetail>(), cancellationToken),
            Times.Exactly(quoteRequest.QuoteBeerItemDto.Count));
    }

    [Fact]
    [Trait("categories", "Generate Quote")]
    public async Task GenerateQuote_WithInvalidWholesaler_ThrowsInvalidOperationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var quoteRequest = new QuoteResquestDto { WholesalerId = 1 };
        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
        wholesalerRepositoryMock.Setup(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken))
            .Returns(Task.FromResult<Wholesaler>(null!));

        var wholesalerService = new WholesalerService(
            wholesalerRepositoryMock.Object,
            Mock.Of<IStockRepository>(), 
            Mock.Of<IBeerRepository>(),
            Mock.Of<IQuoteRequestRepository>(),
            Mock.Of<IQuoteRequestBeerDetailsRepository>());

        // Act & Assert
        async Task GenerateQuote() => await wholesalerService.GenerateQuote(quoteRequest, cancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(GenerateQuote);

        // Verify repository method call
        wholesalerRepositoryMock.Verify(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken), Times.Once);
    }

    [Fact]
    [Trait("categories", "Generate Quote")]
    public async Task GenerateQuote_WithInsufficientStock_ThrowsInvalidOperationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var quoteRequest = new QuoteResquestDto
        {
            WholesalerId = 1,
            QuoteBeerItemDto = new List<QuoteBeerItemRequestDto>
            {
                new QuoteBeerItemRequestDto { BeerId = 1, Quantity = 15 }
            }
        };

        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
        wholesalerRepositoryMock.Setup(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken))
            .ReturnsAsync(new Wholesaler { WholesalerId = quoteRequest.WholesalerId });

        var stockRepositoryMock = new Mock<IStockRepository>();
        stockRepositoryMock.Setup(repo => repo.GetStockByBeerAndWholesalerId(It.IsAny<int>(), It.IsAny<int>(), cancellationToken))
            .ReturnsAsync((Stock)null!);

        var wholesalerService = new WholesalerService(
            wholesalerRepositoryMock.Object,
            stockRepositoryMock.Object,
            Mock.Of<IBeerRepository>(), 
            Mock.Of<IQuoteRequestRepository>(),
            Mock.Of<IQuoteRequestBeerDetailsRepository>());

        // Act & Assert
        async Task GenerateQuote() => await wholesalerService.GenerateQuote(quoteRequest, cancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(GenerateQuote);

        // Verify repository method calls
        wholesalerRepositoryMock.Verify(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken), Times.Once);
        stockRepositoryMock.Verify(
            repo => repo.GetStockByBeerAndWholesalerId(It.IsAny<int>(), It.IsAny<int>(), cancellationToken),
            Times.Never);
    }

    [Fact]
    [Trait("categories", "Generate Quote")]
    public async Task GenerateQuote_WithInvalidBeer_ThrowsInvalidOperationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var quoteRequest = new QuoteResquestDto
        {
            WholesalerId = 1,
            QuoteBeerItemDto = new List<QuoteBeerItemRequestDto>
            {
                new QuoteBeerItemRequestDto { BeerId = 5, Quantity = 5 }
            }
        };

        var wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
        wholesalerRepositoryMock.Setup(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken))
            .ReturnsAsync(new Wholesaler { WholesalerId = quoteRequest.WholesalerId });

        var stockRepositoryMock = new Mock<IStockRepository>();
        stockRepositoryMock.Setup(repo => repo.GetStockByBeerAndWholesalerId(It.IsAny<int>(), It.IsAny<int>(), cancellationToken))
            .ReturnsAsync(new Stock { QuantityStock = 10 }); 

        var beerRepositoryMock = new Mock<IBeerRepository>();
        beerRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), cancellationToken))
            .Returns(Task.FromResult<Beer>(null!));

        var wholesalerService = new WholesalerService(
            wholesalerRepositoryMock.Object,
            stockRepositoryMock.Object,
            beerRepositoryMock.Object,
            Mock.Of<IQuoteRequestRepository>(), 
            Mock.Of<IQuoteRequestBeerDetailsRepository>());

        // Act & Assert
        async Task GenerateQuote() => await wholesalerService.GenerateQuote(quoteRequest, cancellationToken);
        await Assert.ThrowsAsync<InvalidOperationException>(GenerateQuote);

        // Verify repository method calls
        wholesalerRepositoryMock.Verify(repo => repo.GetByIdAsync(quoteRequest.WholesalerId, cancellationToken), Times.Once);
        stockRepositoryMock.Verify(
            repo => repo.GetStockByBeerAndWholesalerId(It.IsAny<int>(), It.IsAny<int>(), cancellationToken),
            Times.Never);
    }
}


