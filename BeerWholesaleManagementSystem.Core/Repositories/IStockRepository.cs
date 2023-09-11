using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Repositories;

/// <summary>
///  Define the Stock Repository Interface
/// </summary>
public interface IStockRepository : IRepository<Stock>
{
    /// <summary>
    /// Get Stock By Beer And WholesalerId asynchronously
    /// </summary>
    /// <param name="beerId"></param>
    /// <param name="wholesalerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<Stock> GetStockByBeerAndWholesalerId(int beerId, int wholesalerId, CancellationToken cancellationToken);
}
