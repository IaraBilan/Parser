using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    class DALContex : DbContext
    {
        public DbSet<Deal> Deals { get; set; }
        public DALContex()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=localdb;Username=postgres;Password=Yaroslavna123");
        }
    }
}
