using Aplication.Services.Products.Favourites.Commands.Delete;
using Application.Test.Builders;
using Domain.Dtos;
using Domain.Products;
using Persistance.Contexts;

namespace Application.Test.ProductFavouriteTest
{
    public class DeleteTest
    {
        [Fact]
        public async Task Handle_Should_Delete_Favourite_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
			await AddFavourite(context);
            var handler = new DeleteFavouriteHandler(context);

            var request = new DeleteFavouriteCommand(1);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto>(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(context.ProductItemFavourites.Local);
            Assert.Equal("Delete Favourite Is Success", result.Message.First());
        }
		private async Task AddFavourite(DataBaseContext context)
		{
			context.Products.Add(new ProductItem
            {
                Id = 1,
				Name = "test",
			    Price = 10000
            });
            context.ProductItemFavourites.Add(new ProductItemFavourite 
            {
                Id = 1,
                ProductItemId = 1,
                UserId = "fbcb56c5-e0fd-4e1b-bb12-c6ea78b2657c"
            });
		}
	}
}
