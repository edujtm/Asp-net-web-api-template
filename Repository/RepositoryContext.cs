using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerSeed());
            modelBuilder.ApplyConfiguration(new VehicleSeed());
            modelBuilder.ApplyConfiguration(new BookingSeed());
        }


        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Vehicle>? Vehicles { get; set; }

    }
}