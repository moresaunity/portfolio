using Domain.Discounts;
using Domain.Order;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<ProductItem> Products { get; set; }
        DbSet<ProductBrand> ProductBrands { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<DiscountUsageHistory> DiscountUsageHistorys { get; set; }
        DbSet<ProductItemFavourite> ProductItemFavourites { get; set; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
