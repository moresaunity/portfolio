﻿using Application.Services.Products.Queries;

namespace Api.EndPoint.Models.Dtos.Product.ProductItem.Get
{
    public class ProductItemGetRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? ProductTypeId { get; set; }
        public int[]? BrandId { get; set; }
        public bool AvailableStock { get; set; } = false;
        public string? SearchKey { get; set; }
        public SortType SortType { get; set; } = SortType.None;
    }
}
