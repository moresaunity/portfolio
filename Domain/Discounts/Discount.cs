using Domain.Attributes;
using Domain.Products;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Discounts
{
    [Auditable]
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool UsePersentage { get; set; }
        public int DiscountPersentage { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool RequiresCouponCode { get; set; }
        public string CouponCode { get; set; }
        [NotMapped]
        public DiscountType DiscountType 
        {
            get => (DiscountType)this.DisCountTypeId;
            set => this.DisCountTypeId = (int)value;
        }
        public int DisCountTypeId { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; }
        public ICollection<ProductBrand> ProductBrands { get; set; }
        public ICollection<User> Users { get; set; }
        public int LimitationTimes { get; set; }
        [NotMapped]
        public DiscountLimitationType DiscountLimitationType
        {
            get => (DiscountLimitationType)this.DiscountLimitationTypeId;
            set => this.DiscountLimitationTypeId = (int)value;
        }
        public int DiscountLimitationTypeId { get; set; }
        
        public int GetDiscountAmount(int amount)
        {
            var result = 0;
            if(UsePersentage)
            {
                result = (((amount) * (DiscountPersentage)) / 100);
            }
            else
            {
                result = DiscountAmount;
            }
            return result;
        }
    }
    public enum DiscountType
    {
        AssignedProduct = 0,
        AssignedToCategories = 1,
        AssignedToUser = 2,
        AssignedToBrand = 3
    }
    public enum DiscountLimitationType
    {
        Unlimited = 0,
        NTimesOnly = 1,
        NTimesPerCustomer = 2
    }
}
