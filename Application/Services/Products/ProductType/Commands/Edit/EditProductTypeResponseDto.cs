namespace Aplication.Services.Products.ProductType.Commands.Edit
{
    public class EditProductTypeResponseDto
    {
        public int Id { get; set; }
        public required string ProductType { get; set; }
        public int? ParentProductTypeId { get; set; }
    }

}
