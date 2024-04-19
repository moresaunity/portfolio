using Domain.Products;

namespace Api.EndPoint.Models.Dtos.Product.Type.GetAll
{
    public class GetProductTypeResultDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentProductTypeId { get; set; }
        public ProductType ParentProductType { get; set; }
        public ICollection<ProductType> SubType { get; set; }
        public List<Links> Links { get; set; }
    }
}
