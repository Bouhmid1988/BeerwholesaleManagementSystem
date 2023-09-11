using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services
{
    /// <summary>
    /// Represents a service for managing wholesaler-related operations.
    /// </summary>
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IQuoteRequestBeerDetailsRepository _quoteRequestBeerDetailsRepository;

        public WholesalerService(
            IWholesalerRepository wholesalerRepository,
            IStockRepository stockRepository,
            IBeerRepository beerRepository,
            IQuoteRequestRepository quoteRequestRepository,
            IQuoteRequestBeerDetailsRepository quoteRequestBeerDetailsRepository)
        {
            _wholesalerRepository = wholesalerRepository ?? throw new ArgumentNullException(nameof(wholesalerRepository));
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _beerRepository = beerRepository ?? throw new ArgumentNullException(nameof(beerRepository));
            _quoteRequestRepository = quoteRequestRepository ?? throw new ArgumentNullException(nameof(quoteRequestRepository));
            _quoteRequestBeerDetailsRepository = quoteRequestBeerDetailsRepository ?? throw new ArgumentNullException(nameof(quoteRequestBeerDetailsRepository));
        }

        /// <inheritdoc cref="IWholesalerService.GenerateQuote"/>
        public async Task<QuoteResponseDto> GenerateQuote(QuoteResquestDto quoteRequest, CancellationToken cancellationToken)
        {
            await EnsureWholesalerExists(quoteRequest, cancellationToken);
            await EnsureAllBeersSoldByWholesalerAsync(quoteRequest, cancellationToken);
            await EnsureOrderQuantityWithinStock(quoteRequest, cancellationToken);
            var totalPrice = await CalculateTotalPrice(quoteRequest, cancellationToken);
            ApplyDiscounts(ref totalPrice, quoteRequest);

            var quoteRequestDto = new QuoteResponseDto
            {
                WholesalerId = quoteRequest.WholesalerId,
                RequestDate = quoteRequest.RequestDate,
                QuoteItems = new List<QuoteBeerItemResponsetDto>(),
                Statuts = "in progress",
                TotalPrice = totalPrice
            };

            var QuoteRequestEntity = new QuoteRequest
            {
                WholesalerId = quoteRequest.WholesalerId,
                DateQuote = quoteRequest.RequestDate,
                Statuts = "in progress",
                TotalPrice = totalPrice
            };

            await _quoteRequestRepository.AddAsync(QuoteRequestEntity, cancellationToken);
            var uniqueBeerIds = quoteRequest.QuoteBeerItemDto.Select(beerDetail => beerDetail.BeerId).Distinct();
            var quoteBeerItems = new List<QuoteBeerItemResponsetDto>();

            foreach (var beerId in uniqueBeerIds)
            {
                var quantity = quoteRequest.QuoteBeerItemDto
                    .Where(beerDetail => beerDetail.BeerId == beerId)
                    .Sum(beerDetail => beerDetail.Quantity);

                var beer = await _beerRepository.GetByIdAsync(beerId, cancellationToken);

                var quoteBeerItemDto = new QuoteBeerItemResponsetDto
                {
                    BeerId = beerId,
                    Name = beer.Name,
                    BreweryId = beer.BreweryId,
                    Quantity = quantity,
                    Price = beer.Price
                };

                quoteBeerItems.Add(quoteBeerItemDto);

                quoteRequestDto.QuoteItems.Add(quoteBeerItemDto);

                var QuoteRequestBeerDetail = new QuoteRequestBeerDetail
                {
                    QuoteRequestId = QuoteRequestEntity.QuoteRequestId,
                    BeerId = beerId,
                    Quantity = quantity
                };

                await _quoteRequestBeerDetailsRepository.AddAsync(QuoteRequestBeerDetail, cancellationToken);
            }

            return quoteRequestDto;
        }

        /// <summary>
        /// Calculates the total price of the quoted beers based on their quantities and prices.
        /// </summary>
        /// <param name="quoteRequest">The quote request containing beer details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The total price of the quoted beers.</returns>
        private async Task<decimal> CalculateTotalPrice(QuoteResquestDto quoteRequest, CancellationToken cancellationToken)
        {
            decimal totalPrice = 0;

            foreach (var beerDetail in quoteRequest.QuoteBeerItemDto)
            {
                var beer = await _beerRepository.GetByIdAsync(beerDetail.BeerId, cancellationToken);
                if (beer != null)
                    totalPrice += beer.Price * beerDetail.Quantity;
            }

            return totalPrice;
        }

        /// <summary>
        /// Ensures that the order quantity for each beer is within the available stock.
        /// </summary>
        /// <param name="quoteRequests">The quote request containing beer details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidOperationException">Thrown if the order quantity exceeds available stock.</exception>
        private async Task EnsureOrderQuantityWithinStock(QuoteResquestDto quoteRequests, CancellationToken cancellationToken)
        {
            foreach (var beerDetail in quoteRequests.QuoteBeerItemDto)
            {
                var stock = await _stockRepository.GetStockByBeerAndWholesalerId(beerDetail.BeerId, quoteRequests.WholesalerId, cancellationToken);
                if (stock == null)
                    throw new Exception("Stock not found for the specified beer and wholesaler.");
                if (beerDetail.Quantity > stock.QuantityStock)
                    throw new InvalidOperationException($"The quantity of beer ordered ({beerDetail.Quantity}) exceeds wholesaler's stock ({stock.QuantityStock}).");
            }
        }

        /// <summary>
        /// Ensures that all beers in the order are sold by the specified wholesaler.
        /// </summary>
        /// <param name="QuoteRequest">The quote request containing beer details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidOperationException">Thrown if not all beers in the order are sold by the wholesaler.</exception>
        private async Task EnsureAllBeersSoldByWholesalerAsync(QuoteResquestDto QuoteRequest, CancellationToken cancellationToken)
        {
            var wholesalerId = QuoteRequest.WholesalerId;
            var beerIds = QuoteRequest.QuoteBeerItemDto.Select(detail => detail.BeerId).ToList();
            foreach (var beerId in beerIds)
            {
                var wholesaler = await _wholesalerRepository.GetBeerByWholesalerAndBeerIdAsync(wholesalerId, beerId, cancellationToken);
                if (wholesaler == null)
                    throw new InvalidOperationException("Not all beers in the order are sold by the wholesaler.");
            }
        }

        /// <summary>
        /// Ensures that the specified wholesaler exists.
        /// </summary>
        /// <param name="QuoteRequest">The quote request containing the wholesaler ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidOperationException">Thrown if the wholesaler does not exist.</exception>
        private async Task EnsureWholesalerExists(QuoteResquestDto QuoteRequest, CancellationToken cancellationToken)
        {
            var existingWholesaler = await _wholesalerRepository.GetByIdAsync(QuoteRequest.WholesalerId, cancellationToken);
            if (existingWholesaler == null)
                throw new InvalidOperationException("The wholesaler must exist.");
        }


        /// <summary>
        /// Apply discounts to the total price based on the beer count.
        /// </summary>
        /// <param name="totalPrice">The total price to apply discounts to.</param>
        /// <param name="quoteRequest">The quote request containing beer details.</param>
        private void ApplyDiscounts(ref decimal totalPrice, QuoteResquestDto quoteRequest)
        {
            var beerCount = quoteRequest.QuoteBeerItemDto.Sum(beerDetail => beerDetail.Quantity);

            if (beerCount > 20)
            {
                totalPrice *= 0.8m;
            }
            else if (beerCount > 10)
            {
                totalPrice *= 0.9m;
            }
        }
    }
}
