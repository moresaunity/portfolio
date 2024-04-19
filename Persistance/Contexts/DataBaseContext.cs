using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Discounts;
using Domain.Products;
using Domain.Products.Order;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Contexts
{
    public class DataBaseContext: DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<ProductItem> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountUsageHistory> DiscountUsageHistorys { get; set; }
        public DbSet<ProductItemFavourite> ProductItemFavourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (Attribute.IsDefined(entityType.ClrType, typeof(AuditableAttribute)))
                {
                    modelBuilder.Entity(entityType.ClrType).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityType.ClrType).Property<bool>("IsRemove").HasDefaultValue(false);
                }
            }
            //        modelBuilder.Entity<ProductType>()
            //.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            modelBuilder.ApplyConfiguration(new ProductBrandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeEntityTypeConfiguration());

            //DataBaseContextSeed.ProductSeed(modelBuilder);

            modelBuilder.Entity<Order>().OwnsOne(p => p.Address);

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var modifiendEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Detached);
            foreach (var entry in modifiendEntries)
            {
                var entityType = entry.Context.Model.FindEntityType(entry.Entity.GetType());
                var inserted = entityType.FindProperty("InsertTime");
                var updateTime = entityType.FindProperty("UpdateTime");
                var removeTime = entityType.FindProperty("RemoveTime");
                var isRemove = entityType.FindProperty("IsRemove");
                if (entry.State == EntityState.Added && inserted != null)
                {
                    entry.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified && updateTime != null)
                {
                    entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Deleted && removeTime != null && isRemove != null)
                {
                    entry.Property("RemoveTime").CurrentValue = DateTime.Now;
                    entry.Property("IsRemove").CurrentValue = true;
                    entry.State = EntityState.Modified;
                }
            }
            return base.SaveChanges();
        }
    }
}
