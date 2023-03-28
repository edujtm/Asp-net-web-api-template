using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerSeed());
            modelBuilder.ApplyConfiguration(new VehicleSeed());
            modelBuilder.ApplyConfiguration(new BookingSeed());
            modelBuilder.ApplyConfiguration(new RoleConfig());
        }


        public DbSet<Booking>? Bookings { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Vehicle>? Vehicles { get; set; }

    }
}