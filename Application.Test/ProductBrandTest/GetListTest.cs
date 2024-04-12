using Application.Test.Builders;
using AutoMapper;
using Infrastructure.MappingProfile;
using Domain.Dtos;
using Application.Services.Products.Queries;
using Persistance.Contexts;
using Domain.Products;
using Application.Services.Products.Brands.Queries.Get;

namespace Application.Test.ProductBrandTest
{
    public class GetListTest
    {
        [Fact]
        public async Task Handle_Should_GetAll_Brand_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new GetCommentOfCatalogItemQuery(context);

            await AddBrand(context);

            // Act
            GetBrandRequest request = new GetBrandRequest();
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<List<GetBrandDto>>>(result);
            Assert.Equal(2, result.Data.Count);
        }
        private async Task AddBrand(DataBaseContext context)
        {
            context.ProductBrands.Add(new ProductBrand
            {
                Id = 1,
                Brand = "test"
            });
            context.ProductBrands.Add(new ProductBrand
            {
                Id = 2,
                Brand = "test2"
            });
        }
    }
}
