using Domain.Attributes;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    [Auditable]
    public class ProductBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public ICollection<Discount> Discounts { get; set; }
    }
}
