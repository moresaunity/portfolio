using Application.Test.Builders;
using AutoMapper;
using Infrastructure.MappingProfile;
using Domain.Dtos;
using Application.Services.Products.Queries;
using Persistance.Contexts;
using Domain.Products;

namespace Application.Test.ProductItemTest
{
    public class GetListTest
    {
        [Fact]
        public async Task Handle_Should_GetAll_Product_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new GetCommentOfProductItemQuery(context, mapper);

            await AddProduct(context);

            // Act
            GetProductItemRequestDto requstDto = new GetProductItemRequestDto();
            GetProductItemRequest request = new GetProductItemRequest(requstDto);
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<List<GetProductItemDto>>>(result);
        }
        private async Task AddProduct(DataBaseContext context)
        {
            context.ProductTypes.Add(new ProductType
            {
                Id = 1,
                Type = "test"
            });
            context.ProductBrands.Add(new ProductBrand
            {
                Id = 1,
                Brand = "test"
            });
            context.Products.Add(new ProductItem
            {
                Id = 1,
                Name = "test",
                Description = "test",
                ProductBrandId = 1,
                ProductTypeId = 1,
                Price = 12000,
                Slug = "test",
                AvailableStock = 12,
                MaxStockThreshold = 12,
                RestockThreshold = 12,
                ProductItemImages = new List<ProductItemImage>
                {
                    new ProductItemImage
                    {
                        Id = 1,
                        Src = "test",
                        ProductItemId = 1
                    }
                },
                ProductItemtblFeatures = new List<ProductItemFeature>
                {
                    new ProductItemFeature {
                        Id = 1,
                        ProductItemId= 1,
                        Group = "test",
                        Key = "test",
                        Value = "test"
                    }
                }
            });
        }
    }
}
