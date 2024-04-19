using Aplication.Services.Products.Favourites.Commands.Edit;
using Application.Test.Builders;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using Infrastructure.MappingProfile;
using Persistance.Contexts;

namespace Application.Test.ProductFavouriteTest
{
    public class EditTest
    {
        [Fact]
        public async Task Handle_Should_Edit_Favourite_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
			await AddFavourite(context);
			var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new EditCommentHandler(context);

            var request = new EditFavouriteCommand(new EditFavouriteDto
            {
				ProductId = 1,
                UserId = "fbcb56c5-e0fd-4e1b-bb12-c6ea78b2657c"
            }, 1);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<EditFavouriteResponseDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Edit Favourite Is Success", result.Message.First());
			Assert.Equal(request.FavouriteDto.UserId, result.Data.UserId);
			Assert.Equal(request.FavouriteDto.ProductId, result.Data.ProductId);
            Assert.Equal(1, result.Data.Id); // Assuming the entity ID is 1
        }
		private async Task AddFavourite(DataBaseContext context)
		{
			context.Products.Add(new ProductItem
			{
				Id = 1,
				Name = "test",
			    Price = 12000
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
