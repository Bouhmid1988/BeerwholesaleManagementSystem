﻿using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;

namespace BeerWholesaleManagementSystem.Data.Repositories;

/// <summary>
/// Repository for performing operations related to sales of beer.
/// </summary>
public class SaleBeerRepositories : Repository<SaleBeer>, ISaleBeerRepositories
{
    private readonly BeerDbContext _beerDbContext;
    public SaleBeerRepositories(BeerDbContext context) : base(context)
    {
        _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
}
