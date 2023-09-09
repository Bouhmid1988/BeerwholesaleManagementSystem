using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Services;

public interface IStockService
{
    Task<Stock> GetStockById(int id, CancellationToken cancellationToken);
    Task<Stock> UpdateStockQuantity(int stockId, int newQuantity, CancellationToken cancellationToken);
}
