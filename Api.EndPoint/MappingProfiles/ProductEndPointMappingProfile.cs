using Api.EndPoint.Models.Dtos.Product.ProductItem;
using Aplication.Services.Products.ProductItem.Commands.Create;
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
        }
    }
}
