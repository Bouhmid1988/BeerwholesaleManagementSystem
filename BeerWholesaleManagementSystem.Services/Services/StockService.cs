using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services
{
    /// <summary>
    /// Represents a service for managing stock-related operations.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepositories;

        public StockService(IStockRepository stockRepositories)
        {
            _stockRepositories = stockRepositories ?? throw new ArgumentNullException(nameof(_stockRepositories));
        }

        /// <inheritdoc cref="IStockService.GetStockById"/>
        public async Task<Stock> GetStockById(int id, CancellationToken cancellationToken)
        {
            return await _stockRepositories.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc cref="IStockService.UpdateStockQuantity"/>
        public async Task<Stock> UpdateStockQuantity(int stockId, int newQuantity, CancellationToken cancellationToken)
        {
            var existingStock = await _stockRepositories.GetByIdAsync(stockId, cancellationToken);

            if (existingStock == null)
                throw new InvalidOperationException("The specified stock was not found.");
            
            existingStock.QuantityStock = newQuantity;
            await _stockRepositories.UpdateAsync(existingStock, cancellationToken);
            return existingStock;
        }
    }
}
