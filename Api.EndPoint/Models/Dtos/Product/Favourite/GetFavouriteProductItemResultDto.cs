using Domain.Discounts;
using Domain.Products;
using Domain.Products.Order;

namespace Api.EndPoint.Models.Dtos.Product.Favourite
{
    public class GetFavouriteProductItemResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public int? PercentDiscount { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string Slug { get; set; }
        public int VisitCount { get; set; }
        public ICollection<ProductItemFeature> ProductItemtblFeatures { get; set; }
        public ICollection<ProductItemImage> ProductItemImages { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<ProductItemFavourite> ProductItemFavourites { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public List<Links> Links { get; set; }
    }
}
