using BeerWholesaleManagementSystem.Core.DTO;

namespace BeerWholesaleManagementSystem.Core.Services;

/// <summary>
/// Defines the Wholesaler Service interface.
/// </summary>
public interface IWholesalerService
{
    /// <summary>
    /// Generates a quote for a wholesale request.
    /// </summary>
    /// <param name="request">The details of the quote request.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The generated quote response.</returns>
    Task<QuoteResponseDto> GenerateQuote(QuoteResquestDto request, CancellationToken cancellationToken);
}