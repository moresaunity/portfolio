namespace Api.EndPoint.Models.Dtos.Product.Type.Edit
{
    public class EditProductTypeRequestDto
    {
        public string ProductType { get; set; }
        public int? ParentProductTypeId { get; set; }
    }
}
