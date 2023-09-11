using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories
{
    /// <summary>
    /// Repository for performing operations related to beer stock.
    /// </summary>
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly BeerDbContext _beerDbContext;

        public StockRepository(BeerDbContext context) : base(context)
        {
            _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IStockRepository.GetStockByBeerAndWholesalerId"/>
        public async Task<Stock> GetStockByBeerAndWholesalerId(int beerId, int wholesalerId, CancellationToken cancellationToken)
        {
            var stock = await _beerDbContext.Stock
                .Where(s => s.BeerId == beerId && s.WholesalerId == wholesalerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (stock == null)
                throw new Exception("Stock not found for the specified beer and wholesaler.");

            return stock;
        }
    }
}
