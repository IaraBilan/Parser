using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DLContext : DbContext
    {
        public DbSet<Deal> Deals { get; set; }
        public DLContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Test;Username=postgres;Password=Yaroslavna123");
        }
    }
}
