using Domain.Dtos;
using Application.Interfaces.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Discounts;
using Domain.Products;
using Domain.Products.Order;

namespace Application.Services.Products.Favourites.Queries
{
    public class GetFavouriteByIdRequest : IRequest<BaseDto<GetFavouriteByIdResponseDto>>
    {
        public GetFavouriteByIdCommand favouriteDto;

        public GetFavouriteByIdRequest(GetFavouriteByIdCommand FavouriteDto)
        {
            favouriteDto = FavouriteDto;
        }
    }
    public class GetFavouriteByIdHandler : IRequestHandler<GetFavouriteByIdRequest, BaseDto<GetFavouriteByIdResponseDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetFavouriteByIdHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<GetFavouriteByIdResponseDto>> Handle(GetFavouriteByIdRequest request, CancellationToken cancellationToken)
        {
            int rowCount = 0;
            ProductItemFavourite model = context.ProductItemFavourites
                .Include(p => p.ProductItem).ThenInclude(p => p.Discounts)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductBrand)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductType)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductItemImages)
                .Include(p => p.ProductItem).ThenInclude(p => p.ProductItemtblFeatures)
                .FirstOrDefault(f => f.Id == request.favouriteDto.Id);

            GetFavouriteByIdResponseDto data = mapper.Map<GetFavouriteByIdResponseDto>(model);
            data.Product = mapper.Map<GetFavouriteProductItemByIdResponseDto>(model.ProductItem);
            data.Product.ProductItemFavourites = null;
            foreach (var i in data.Product.ProductItemtblFeatures)
            {
                i.ProductItem = null;
            }
            foreach (var i in data.Product.ProductItemImages)
            {
                i.ProductItem = null;
            }
            return Task.FromResult(new BaseDto<GetFavouriteByIdResponseDto>(true, new List<string> { "Get Favourites By Id Is Success" }, data));
        }
    }
    public class GetFavouriteByIdCommand
    {
        public int Id { get; set; }
    }
    public class GetFavouriteByIdResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public GetFavouriteProductItemByIdResponseDto Product { get; set; }
        public int ProductItemId { get; set; }
    }
    public class GetFavouriteProductItemByIdResponseDto
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
