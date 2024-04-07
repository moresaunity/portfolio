using Domain.Attributes;

namespace Domain.Products
{
    [Auditable]
    public class ProductItemImage
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public ProductItem ProductItem { get; set; }
        public int ProductItemId { get; set; }
    }
}