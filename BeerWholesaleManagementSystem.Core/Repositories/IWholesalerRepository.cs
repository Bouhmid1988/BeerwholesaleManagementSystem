using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Repositories;
/// <summary>
/// Define the Wholesaler Repository Interface
/// </summary>
public interface IWholesalerRepository : IRepository<Wholesaler>
{
    /// <summary>
    /// Get Beer By Wholesaler And BeerId asynchronously
    /// </summary>
    /// <param name="wholesalerId"></param>
    /// <param name="beerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<Stock> GetBeerByWholesalerAndBeerIdAsync(int wholesalerId, int beerId, CancellationToken cancellationToken);
}
