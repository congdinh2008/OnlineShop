using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Data
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        #region Override SaveChanges

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IEntity entityBase)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entityBase.UpdatedAt = now;
                            break;

                        case EntityState.Added:
                            entityBase.InsertedAt = now;
                            entityBase.UpdatedAt = now;
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
