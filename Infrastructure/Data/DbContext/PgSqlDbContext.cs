using Domain.Entities;
using Infrastructure.Data.EntitiesConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Data.DbContext
{
    public class PgSqlDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PgSqlDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating((modelBuilder));
            modelBuilder.ApplyConfiguration(new UserConfig());
        }

        public DbSet<User> Users { get; set; }
    }
}