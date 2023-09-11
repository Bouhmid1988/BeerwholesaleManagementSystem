using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services
{
    /// <summary>
    /// Represents a service for managing sale beer-related operations.
    /// </summary>
    public class SaleBeerService : ISaleBeerService
    {
        private readonly ISaleBeerRepositories _saleBeerRepositories;
        private readonly IWholesalerRepository _wholesalerRepositories;
        private readonly IBeerRepository _beerRepositores;

        public SaleBeerService(ISaleBeerRepositories saleBeerRepositories, IBeerRepository beerRepositories, IWholesalerRepository wholesalerRepositories)
        {
            _saleBeerRepositories = saleBeerRepositories ?? throw new ArgumentNullException(nameof(_saleBeerRepositories));
            _beerRepositores = beerRepositories ?? throw new ArgumentNullException(nameof(_beerRepositores));
            _wholesalerRepositories = wholesalerRepositories ?? throw new ArgumentNullException(nameof(_wholesalerRepositories));
        }

        /// <inheritdoc cref="ISaleBeerService.CreateSaleBeer"/>
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
}
