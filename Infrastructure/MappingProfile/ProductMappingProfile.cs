using Aplication.Services.Products.ProductItem.Commands.Create;
using Aplication.Services.Products.ProductItem.Commands.Edit;
using Aplication.Services.Products.ProductType.Commands.Edit;
using Application.Services.Products.Favourites.Queries;
using Application.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.ProductType.Queries.Get;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Products;

namespace Infrastructure.MappingProfile
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductItem, CreateProductItemDto>().ReverseMap();
            CreateMap<ProductItem, GetProductItemDto>()
                .ForMember(dest => dest.Images, opt =>
                 opt.MapFrom(src => src.ProductItemImages.Select(p => p.Src)))
            .ReverseMap();
            CreateMap<ProductItem, GetByIdProductItemDto>().ReverseMap();
            CreateMap<ProductItem, EditProductItemDto>().ReverseMap();
            CreateMap<ProductItem, EditProductItemResultDto>().ReverseMap();
            CreateMap<ProductItem, GetFavouriteProductItemResponseDto>().ReverseMap();
            CreateMap<ProductItem, GetFavouriteProductItemByIdResponseDto>().ReverseMap();

            CreateMap<ProductType, GetProductTypeByIdDto>().ReverseMap();
            CreateMap<ProductType, EditProductTypeResponseDto>().ReverseMap();

            CreateMap<ProductItemFavourite, GetFavouriteResponseDto>().ReverseMap();
            CreateMap<ProductItemFavourite, GetFavouriteByIdResponseDto>().ReverseMap();
        }
    }
}
