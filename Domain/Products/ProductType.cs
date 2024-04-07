using Domain.Attributes;
using Domain.Discounts;

namespace Domain.Products
{
    [Auditable]
    public class ProductType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int? ParentProductTypeId { get; set; }
        public ProductType ParentProductType { get; set; }
        public ICollection<ProductType> SubType { get; set; }
        public ICollection<Discount> Discounts { get; set; }

    }
}
