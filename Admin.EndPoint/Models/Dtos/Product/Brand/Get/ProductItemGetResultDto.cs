﻿
namespace Admin.EndPoint.Models.Dtos.Product.Brand.Get
{
    public class ProductBrandGetResultDto
	{
        public int Id { get; set; }
        public string Brand { get; set; }
        public List<Links> Links { get; set; }
    }
}
