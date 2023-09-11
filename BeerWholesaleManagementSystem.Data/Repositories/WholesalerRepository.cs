using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories
{
    /// <summary>
    /// Repository for performing operations related to wholesalers.
    /// </summary>
    public class WholesalerRepository : Repository<Wholesaler>, IWholesalerRepository
    {
        private readonly BeerDbContext _beerDbContext;

        public WholesalerRepository(BeerDbContext context) : base(context)
        {
            _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IWholesalerRepository.GetBeerByWholesalerAndBeerIdAsync"/>
        public async Task<Stock> GetBeerByWholesalerAndBeerIdAsync(int wholesalerId, int beerId, CancellationToken cancellationToken)
        {
            var stockEntry = await _beerDbContext.Stock
              .FirstOrDefaultAsync(stock =>
              stock.WholesalerId == wholesalerId &&
              stock.BeerId == beerId,
              cancellationToken);

            if (stockEntry == null)
                throw new Exception("The specified wholesaler does not exist for this beer.");

            return stockEntry;
        }
    }
}
