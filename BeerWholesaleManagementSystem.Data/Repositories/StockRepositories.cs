using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories;

public class StockRepositories : Repositories<Stock>, IStockRepositories
{
    private readonly BeerDbContext _beerDbContext;
    public StockRepositories(BeerDbContext context) : base(context)
    {
        _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
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
