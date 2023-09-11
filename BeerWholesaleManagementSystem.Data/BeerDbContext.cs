using BeerWholesaleManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data
{
    /// <summary>
    /// Represents the database context for managing beer-related entities.
    /// </summary>
    public class BeerDbContext : DbContext
    {
        public BeerDbContext(DbContextOptions<BeerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuoteRequestBeerDetail>()
                .HasKey(cr => new { cr.QuoteRequestId, cr.BeerId });
        }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<QuoteRequest> QuoteRequest { get; set; }
        public DbSet<Wholesaler> Wholesaler { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<SaleBeer> SaleBeer { get; set; }
        public DbSet<QuoteRequestBeerDetail> QuoteRequestBeerDetail { get; set; }
    }
}
