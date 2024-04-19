using Domain.Dtos;
using Application.Interfaces.Contexts;
using Application.Services.UriComposer;
using MediatR;
using Common;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Discounts;
using Domain.Products;
using Domain.Products.Order;

namespace Application.Services.Products.Favourites.Queries
{
    public class GetFavouriteRequest : IRequest<BaseDto<PaginatedItemsDto<GetFavouriteResponseDto>>>
    {
        public GetFavouriteCommand favouriteDto;

        public GetFavouriteRequest(GetFavouriteCommand FavouriteDto)
        {
            favouriteDto = FavouriteDto;
        }
    }
    public class GetFavouriteHandler : IRequestHandler<GetFavouriteRequest, BaseDto<PaginatedItemsDto<GetFavouriteResponseDto>>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetFavouriteHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<PaginatedItemsDto<GetFavouriteResponseDto>>> Handle(GetFavouriteRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ProductItemFavourite> model = context.ProductItemFavourites
                .OrderByDescending(p => p.Id).AsQueryable();

            if (model.Count() == 0) model = context.ProductItemFavourites.Local
                .OrderByDescending(p => p.Id).AsQueryable();

            int rowCount = 0;
            var data = model.Include(p => p.ProductItem).ThenInclude(p => p.Discounts)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductBrand)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductType)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductItemImages)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductItemtblFeatures)
                .PagedResult(request.favouriteDto.Page, request.favouriteDto.PageSize, out rowCount).ToList();

            foreach (var item in data)
            {
                if(item.ProductItem.ProductItemtblFeatures != null)
                    foreach (var i in item.ProductItem.ProductItemtblFeatures)
                    {
                        i.ProductItem = null;
                    }
                if(item.ProductItem.ProductItemImages != null)
                    foreach (var i in item.ProductItem.ProductItemImages)
                    {
                        i.ProductItem = null;
                    }
            }
            List<GetFavouriteResponseDto> mapData = mapper.Map<List<GetFavouriteResponseDto>>(data);
            foreach (var item in mapData)
            {
                item.Product = mapper.Map<GetFavouriteProductItemResponseDto>(data.First(p => p.Id == item.Id).ProductItem);
                item.Product.ProductItemFavourites = null;
            }

            return Task.FromResult(new BaseDto<PaginatedItemsDto<GetFavouriteResponseDto>>(true, new List<string> { "Get Favourites Is Success" }, new PaginatedItemsDto<GetFavouriteResponseDto>(request.favouriteDto.Page, request.favouriteDto.PageSize, rowCount, mapData)));
        }
    }
    public class GetFavouriteCommand
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
    public class GetFavouriteResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public GetFavouriteProductItemResponseDto Product { get; set; }
        public int ProductItemId { get; set; }
    }
    public class GetFavouriteProductItemResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public int? PercentDiscount { get; set; }
        public int ProductTypeId { get; set; }
        public Domain.Products.ProductType ProductType { get; set; }
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
    }
}
