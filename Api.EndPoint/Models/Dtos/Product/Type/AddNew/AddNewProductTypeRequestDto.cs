namespace Api.EndPoint.Models.Dtos.Product.Type.AddNew
{
    public class AddNewProductTypeRequestDto
    {
        public string Type { get; set; }
        public int? ParentProductTypeId { get; set; }
    }
}
