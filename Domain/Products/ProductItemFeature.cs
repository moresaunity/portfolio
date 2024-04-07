using Domain.Attributes;

namespace Domain.Products
{
    [Auditable]
    public class ProductItemFeature
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public ProductItem ProductItem { get; set; }
        public int ProductItemId { get; set; }
    }
}