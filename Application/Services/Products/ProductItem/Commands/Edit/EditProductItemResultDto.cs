using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.ProductItem.Commands.Edit
{
	public class EditProductItemResultDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string? Slug { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public int ProductTypeId { get; set; }
		public int ProductBrandId { get; set; }
		public int AvailableStock { get; set; }
		public int RestockThreshold { get; set; }
		public int MaxStockThreshold { get; set; }
		public List<ProductItemFeature_dto> Features { get; set; }
		public List<ProductItemImage_Dto> Images { get; set; }
	}
}
