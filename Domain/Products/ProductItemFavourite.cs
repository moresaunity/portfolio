using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    [Auditable]
    public class ProductItemFavourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ProductItem ProductItem { get; set; }
        public int ProductItemId { get; set; }
    }
}
