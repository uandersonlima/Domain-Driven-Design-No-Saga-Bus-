using AscoreStore.Catalog.Domain.ProductAggregate;
using AscoreStore.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace AscoreStore.Catalog.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);


            modelBuilder.Entity<Category>().HasData(
                    new Category("Camisa", 0001),
                    new Category("Caneca", 0002),
                    new Category("Relogio", 0003),
                    new Category("Celulares", 0004),
                    new Category("Banho", 0005),
                    new Category("Sapatos", 0006),
                    new Category("Serviços", 0007)
            );

        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateDate") is not null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreateDate").IsModified = false;
                }
            }
            return await base.SaveChangesAsync() > 0;
        }
    }
}