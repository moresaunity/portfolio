using Application.Interfaces.Contexts;
using Application.Services.Products.ProductItem;
using Application.Services.UriComposer;
using AutoMapper;
using Common;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products.Queries
{
    public class GetProductItemRequest: IRequest<BaseDto<List<GetProductItemDto>>>
    {
        public GetProductItemRequestDto Information { get; set; }

        public GetProductItemRequest(GetProductItemRequestDto information)
        {
            Information = information;
        }
    }
    public class GetCommentOfProductItemQuery : IRequestHandler<GetProductItemRequest, BaseDto<List<GetProductItemDto>>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetCommentOfProductItemQuery(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<List<GetProductItemDto>>> Handle(GetProductItemRequest request, CancellationToken cancellationToken)
        {
            int rowCount = 0;
            var query = context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductItemImages)
                .Include(p => p.Discounts)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (request.Information.BrandId != null && request.Information.BrandId.Length != 0)
                query = query.Where(p => request.Information.BrandId.Any(b => b == p.ProductTypeId));
            if (request.Information.ProductTypeId != null)
                query = query.Where(p => p.ProductTypeId == request.Information.ProductTypeId);
            if (!string.IsNullOrEmpty(request.Information.SearchKey))
                query = query.Where(p => p.Name.Contains(request.Information.SearchKey) || p.Description.Contains(request.Information.SearchKey));
            if (request.Information.AvailableStock)
                query = query.Where(p => p.AvailableStock > 0);

            if (request.Information.SortType == SortType.Bestselling)
                query = query.Include(p => p.OrderItems).OrderByDescending(p => p.OrderItems.Count());
            if (request.Information.SortType == SortType.MostPopular)
                query = query.Include(p => p.ProductItemFavourites).OrderByDescending(p => p.ProductItemFavourites.Count());
            if (request.Information.SortType == SortType.MostVisited)
                query = query.OrderByDescending(p => p.VisitCount);
            if (request.Information.SortType == SortType.newest)
                query = query.OrderByDescending(p => p.Id);
            if (request.Information.SortType == SortType.chepest)
                query = query.Include(p => p.Discounts).OrderBy(p => p.Price);
            if (request.Information.SortType == SortType.MostExpensive)
                query = query.Include(p => p.Discounts).OrderByDescending(p => p.Price);

            var data = query.PagedResult(request.Information.Page, request.Information.PageSize, out rowCount).ToList();
            var mediateData = mapper.Map<List<GetProductItemDto>>(data);

                return Task.FromResult(new BaseDto<List<GetProductItemDto>>(true, new List<string> { "Get Products Is Success" }, mediateData));
        }
    }
    public class GetProductItemRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? ProductTypeId { get; set; }
        public int[]? BrandId { get; set; }
        public bool AvailableStock { get; set; } = false;
        public string? SearchKey { get; set; }
        public SortType SortType { get; set; } = SortType.None;
    }
    public class GetProductItemDto
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
    public enum SortType
    {
        None = 0,
        MostVisited = 1,
        Bestselling = 2,
        MostPopular = 3,
        newest = 4,
        chepest = 5,
        MostExpensive = 6
    }
}
