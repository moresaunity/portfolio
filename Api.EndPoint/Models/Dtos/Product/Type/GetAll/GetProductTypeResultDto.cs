using Domain.Products;

namespace Api.EndPoint.Models.Dtos.Product.Type.GetAll
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
