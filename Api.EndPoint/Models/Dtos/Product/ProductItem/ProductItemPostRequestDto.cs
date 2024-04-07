using Application.Services.Products.ProductItem;

namespace Api.EndPoint.Models.Dtos.Product.ProductItem
{
    public class ProductItemPostRequestDto
    {
        public string Name { get; set; }
        public string? Slug { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public List<ProductItemImage_Dto> Images { get; set; }
        public List<ProductItemFeature_dto> Features { get; set; }
    }
}
