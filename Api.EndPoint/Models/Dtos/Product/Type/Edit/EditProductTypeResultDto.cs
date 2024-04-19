using Domain.Products;

namespace Api.EndPoint.Models.Dtos.Product.Type.Edit
{
    public class EditProductTypeResultDto
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public int? ParentProductTypeId { get; set; }
        public List<Links> Links { get; set; }
    }
}
