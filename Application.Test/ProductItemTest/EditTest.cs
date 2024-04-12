using Aplication.Services.Products.ProductItem.Commands.Create;
using Aplication.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.ProductItem.Commands.Edit;
using Application.Test.Builders;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using Infrastructure.MappingProfile;
using Persistance.Contexts;

namespace Application.Test.ProductItemTest
{
    public class EditTest
    {
        [Fact]
        public async Task Handle_Should_Edit_Product_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
			await AddProduct(context);
			var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new EditProductCommentHandler(context, mapper);

            var request = new EditProductItemCommand(new EditProductItemDto
            {
				Name = "Etest",
				Description = "Etest",
				Price = 13000
			}, 1);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<EditProductItemResultDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Edit Product Is Success", result.Message.First());
			Assert.Equal("Etest", result.Data.Name);
			Assert.Equal("Etest", result.Data.Description);
			Assert.Equal(13000, result.Data.Price);
            Assert.Equal(1, result.Data.Id); // Assuming the entity ID is 1
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
