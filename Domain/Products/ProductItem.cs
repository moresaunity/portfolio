using Domain.Attributes;
using Domain.Discounts;
using Domain.Order;

namespace Domain.Products
{
    [Auditable]
    public class ProductItem
    {
        private int _price = 0;
        private int? _oldPrice = null;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get { return GetPrice(); } set { _price = value; } }
        public int? OldPrice { get { return _oldPrice; } set { _oldPrice = value; } }
        public int? PercentDiscount { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string Slug { get; set; }
        public int VisitCount { get; set; }
        public ICollection<ProductItemFeature> ProductItemtblFeatures { get; set; }
        public ICollection<ProductItemImage> ProductItemImages { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<ProductItemFavourite> ProductItemFavourites { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        private int GetPrice()
        {
            var discount = GetPreferrdDiscount(Discounts, _price);
            if (discount != null)
            {
                var discountAmount = discount.GetDiscountAmount(_price);
                int newPrice = _price - discountAmount;
                _oldPrice = _price;
                PercentDiscount = (discountAmount * 100) / _price;
                return newPrice;
            }
            return _price;
        }
        private Discount GetPreferrdDiscount(ICollection<Discount> discounts, int price)
        {
            Discount preferredDiscount = null;
            decimal? maximumDiscountValue = null;
            if (discounts != null)
            {
                foreach (var discount in discounts)
                {
                    var curredDiscountValue = discount.GetDiscountAmount(price);
                    if (curredDiscountValue != decimal.Zero)
                    {
                        if (!maximumDiscountValue.HasValue || curredDiscountValue > maximumDiscountValue)
                        {
                            maximumDiscountValue = curredDiscountValue;
                            preferredDiscount = discount;
                        }
                    }
                }
            }
            return preferredDiscount;
        }
    }
}