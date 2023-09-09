using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;

namespace BeerWholesaleManagementSystem.Data.Repositories;

public class SaleBeerRepositories : Repositories<SaleBeer>, ISaleBeerRepositories
{
    private readonly BeerDbContext _beerDbContext;
    public SaleBeerRepositories(BeerDbContext context) : base(context)
    {
        _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
}
