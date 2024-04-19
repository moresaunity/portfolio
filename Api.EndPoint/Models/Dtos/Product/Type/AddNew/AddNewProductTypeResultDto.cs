namespace Api.EndPoint.Models.Dtos.Product.Type.AddNew
{
    public class AddNewProductTypeResultDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentProductTypeId { get; set; }
        public List<Links> Links { get; set; }
    }
}
