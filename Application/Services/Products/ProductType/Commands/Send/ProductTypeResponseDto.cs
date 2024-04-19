namespace Aplication.Services.Products.ProductType.Commands.Send
{
    public class SendProductTypeResponseDto
    {
            public int Id { get; set; }
            public string Type { get; set; }
            public int? ParentProductTypeId { get; set; }
    }

}
