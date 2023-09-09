using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services;

public class SaleBeerService : ISaleBeerService
{
    private readonly ISaleBeerRepositories _saleBeerRepositories;
    private readonly IWholesalerRepositories _wholesalerRepositories;
    private readonly IBeerRepositories _beerRepositores;

    public SaleBeerService(ISaleBeerRepositories saleBeerRepositories, IBeerRepositories beerRepositories, IWholesalerRepositories wholesalerRepositories)
    {
        _saleBeerRepositories = saleBeerRepositories ?? throw new ArgumentNullException(nameof(_saleBeerRepositories));
        _beerRepositores = beerRepositories ?? throw new ArgumentNullException(nameof(_beerRepositores));
        _wholesalerRepositories = wholesalerRepositories ?? throw new ArgumentNullException(nameof(_wholesalerRepositories));
    }
    public async Task<SaleBeer> CreateSaleBeer(SaleBeer newSaleBeer, CancellationToken cancellationToken)
    {
        var existingBeer = await _beerRepositores.GetByIdAsync(newSaleBeer.BeerId, cancellationToken);
        if (existingBeer == null)
            throw new InvalidOperationException("The specified beer does not exist.");

        var existingWholesaler = await _wholesalerRepositories.GetByIdAsync(newSaleBeer.WholesalerId, cancellationToken);
        if (existingWholesaler == null)
            throw new InvalidOperationException("The specified wholesaler does not exist.");

        var saleBeer = new SaleBeer
        {
            BeerId = newSaleBeer.BeerId,
            WholesalerId = newSaleBeer.WholesalerId,
            Quantity = newSaleBeer.Quantity,
            DateSale = DateTime.UtcNow
        };
        await _saleBeerRepositories.AddAsync(saleBeer, cancellationToken);
        return saleBeer;
    }
}
