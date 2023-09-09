using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services;

public class StockService : IStockService
{
    private readonly IStockRepositories _stockRepositories;

    public StockService(IStockRepositories stockRepositories)
    {
        _stockRepositories = stockRepositories ?? throw new ArgumentNullException(nameof(_stockRepositories));
    }
    public async Task<Stock> GetStockById(int id, CancellationToken cancellationToken)
    {
        return await _stockRepositories.GetByIdAsync(id, cancellationToken);
    }
    public async Task<Stock> UpdateStockQuantity(int stockId, int newQuantity, CancellationToken cancellationToken)
    {
        var existingStock = await _stockRepositories.GetByIdAsync(stockId, cancellationToken);

        if (existingStock == null)
        {
            throw new InvalidOperationException("The specified stock was not found.");
        }

        existingStock.QuantityStock = newQuantity;

        await _stockRepositories.UpdateAsync(existingStock, cancellationToken);
        return existingStock;
    }
}
