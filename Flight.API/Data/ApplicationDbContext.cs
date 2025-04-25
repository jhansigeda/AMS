using Microsoft.EntityFrameworkCore;

namespace Flight.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

       public DbSet<Flight.API.Models.Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight.API.Models.Flight>().HasKey(f => f.Id);
        }
    }
}
