using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltimateASP.Data.Configuration;
using UltimateASP.Data.EntityClasses;

namespace UltimateASP.Data.DatabaseContext
{
    public class HotelListingDBContext : IdentityDbContext<ApiUser>
    {
        public HotelListingDBContext(DbContextOptions options): base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<Country> countries { get; set; }
        public DbSet<Hotel> hotels { get; set; }
    }
}
