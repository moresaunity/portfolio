using Domain.Products;

namespace Admin.EndPoint.Models.Dtos.Product.Type
{
    public class GetProductTypeResultDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentProductTypeId { get; set; }
        public string? ParentType { get; set; }
        public int? CountSubType { get; set; }
        public List<Links> Links { get; set; }
    }
}
