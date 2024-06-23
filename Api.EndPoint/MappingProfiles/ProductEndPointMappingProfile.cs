using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.Favourite;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.GetById;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Post;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Put;
using Api.EndPoint.Models.Dtos.Product.Type.AddNew;
using Api.EndPoint.Models.Dtos.Product.Type.Edit;
using Api.EndPoint.Models.Dtos.Product.Type.GetAll;
using Aplication.Services.Brands.Commands.Delete;
using Aplication.Services.Brands.Commands.Edit;
using Aplication.Services.Products.Brands.Commands.Send;
using Aplication.Services.Products.Favourites.Commands.Edit;
using Aplication.Services.Products.ProductItem.Commands.Create;
using Aplication.Services.Products.ProductItem.Commands.Edit;
using Aplication.Services.Products.ProductType.Commands.Edit;
using Aplication.Services.Products.ProductType.Commands.Send;
using Application.Services.Products.Brands.Queries.Get;
using Application.Services.Products.Brands.Queries.GetById;
using Application.Services.Products.Favourites.Queries;
using Application.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.ProductType.Queries.Get;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Products;

namespace Api.EndPoint.MappingProfiles
{
    public class ProductEndPointMappingProfile: Profile
    {
        public ProductEndPointMappingProfile()
        {
            CreateMap<ProductItemPostRequestDto, CreateProductItemDto>().ReverseMap();
            CreateMap<ProductItemPostRequestDto, ProductItemPostResultDto>().ReverseMap();

            CreateMap<ProductItemGetRequestDto, GetProductItemRequestDto>().ReverseMap();
            CreateMap<GetByIdProductItemDto, ProductItemGetByIdResultDto>().ReverseMap();
            CreateMap<ProductItemGetResultDto, GetProductItemDto>().ReverseMap();

            CreateMap<ProductItemPutRequestDto, EditProductItemDto>().ReverseMap();
            CreateMap<ProductItemPutResultDto, EditProductItemResultDto>().ReverseMap();

            CreateMap<GetBrandDto, ProductBrandGetResultDto>().ReverseMap();
            CreateMap<FindByIdBrandDto, ProductBrandGetResultDto>().ReverseMap();
            CreateMap<SendBrandResponseDto, ProductBrandGetResultDto>().ReverseMap();
            CreateMap<EditBrandResponseDto, ProductBrandGetResultDto>().ReverseMap();
            CreateMap<DeleteBrandResponseDto, ProductBrandGetResultDto>().ReverseMap();

            CreateMap<GetFavouriteResultDto, GetFavouriteResponseDto>().ReverseMap();
            CreateMap<GetFavouriteProductItemResponseDto, GetFavouriteProductItemResultDto>().ReverseMap();
            CreateMap<GetFavouriteProductItemByIdResponseDto, GetFavouriteProductItemResultDto>().ReverseMap();
            CreateMap<GetFavouriteResultDto, GetFavouriteByIdResponseDto>().ReverseMap();

            CreateMap<EditFavouriteResultDto, EditFavouriteResponseDto>().ReverseMap();

            CreateMap<AddNewProductTypeRequestDto, SendProductTypeDto>().ReverseMap();
            CreateMap<AddNewProductTypeResultDto, SendProductTypeResponseDto>().ReverseMap();

            CreateMap<GetProductTypeDto, GetProductTypeResultDto>()
                .ForMember(dest => dest.ParentType, opt =>
                 opt.MapFrom(src => src.ParentProductType.Type))
                .ForMember(dest => dest.CountSubType, opt =>
                 opt.MapFrom(src => src.SubType.Count))
            .ReverseMap();

            CreateMap<GetProductTypeDto, ProductType>().ReverseMap();

            CreateMap<GetProductTypeResultDto, GetProductTypeByIdDto>().ReverseMap();

            CreateMap<EditProductTypeRequestDto, EditProductTypeDto>().ReverseMap();
            CreateMap<EditProductTypeResultDto, EditProductTypeResponseDto>().ReverseMap();
        }
    }
}
