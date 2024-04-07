using Aplication.Services.Products.ProductItem.Commands.Create;
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
            CreateMap<ProductItem, GetProductItemDto>().ReverseMap();
            CreateMap<ProductItem, GetByIdProductItemDto>().ReverseMap();
        }
    }
}
