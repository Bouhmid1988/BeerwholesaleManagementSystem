using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Services;

/// <summary>
/// Define the Stock Service Interface
/// </summary>
public interface IStockService
{
    /// <summary>
    /// Gets a stock by its ID.
    /// </summary>
    /// <param name="id">The ID of the stock.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The corresponding stock.</returns>
    Task<Stock> GetStockById(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the quantity of a stock.
    /// </summary>
    /// <param name="stockId">The ID of the stock.</param>
    /// <param name="newQuantity">The new quantity to set.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The updated stock.</returns>
    Task<Stock> UpdateStockQuantity(int stockId, int newQuantity, CancellationToken cancellationToken);
}
