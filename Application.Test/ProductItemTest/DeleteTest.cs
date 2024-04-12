using Aplication.Services.Products.ProductItem.Commands.Create;
using Aplication.Services.Products.ProductItem.Commands.Delete;
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
    public class DeleteTest
    {
        [Fact]
        public async Task Handle_Should_Delete_Product_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
			await AddProduct(context);
            var handler = new DeleteProductCommentHandler(context);

            var request = new DeleteProductItemCommand(1);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto>(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(context.Products.Local);
            Assert.Equal("Delete Product Is Success", result.Message.First());
        }
		private async Task AddProduct(DataBaseContext context)
		{
			context.Products.Add(new ProductItem
			{
				Id = 1,
				Name = "test",
				Description = "test",
				Price = 12000,
				Slug = "test",
				AvailableStock = 12,
				MaxStockThreshold = 12,
				RestockThreshold = 12,
			});
		}
	}
}
