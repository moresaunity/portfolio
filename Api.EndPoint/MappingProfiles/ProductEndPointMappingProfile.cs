using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.GetById;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Post;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Put;
using Aplication.Services.Brands.Commands.Delete;
using Aplication.Services.Brands.Commands.Edit;
using Aplication.Services.Products.Brands.Commands.Send;
using Aplication.Services.Products.ProductItem.Commands.Create;
using Aplication.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.Brands.Queries.Get;
using Application.Services.Products.Brands.Queries.GetById;
using Application.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.Queries;
using AutoMapper;

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
        }
    }
}
