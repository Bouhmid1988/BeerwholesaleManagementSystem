using BeerWholesaleManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data;
public class BeerDbContext : DbContext
{
    public BeerDbContext(DbContextOptions<BeerDbContext> options) : base(options)
    {

    }
    public DbSet<Beer> Beers { get; set; }
    public DbSet<Brewery> Brewery { get; set; }
    public DbSet<CommandRequest> CommandeRequest { get; set; }
    public DbSet<Wholesaler> Wholesaler { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<SaleBeer> SaleBeer { get; set; }
}


