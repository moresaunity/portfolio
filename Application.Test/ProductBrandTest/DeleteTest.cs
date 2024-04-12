using Aplication.Services.Brands.Commands.Delete;
using Application.Test.Builders;
using Domain.Dtos;
using Domain.Products;
using Persistance.Contexts;

namespace Application.Test.ProductBrandTest
{
    public class DeleteTest
    {
        [Fact]
        public async Task Handle_Should_Delete_Brand_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
			await AddProduct(context);
            var handler = new DeleteBrandHandler(context);

            var request = new DeleteBrandCommand(1);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<DeleteBrandResponseDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(context.ProductBrands.Local);
            Assert.Equal("Delete Brand Is Success", result.Message.First());
        }
		private async Task AddProduct(DataBaseContext context)
		{
			context.ProductBrands.Add(new ProductBrand
			{
				Brand = "test"
			});
		}
	}
}
