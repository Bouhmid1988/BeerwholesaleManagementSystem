using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Repositories;

public interface IStockRepositories : IRepositories<Stock>
{
    Task<Stock> GetStockByBeerAndWholesalerId(int beerId, int wholesalerId, CancellationToken cancellationToken);
}
