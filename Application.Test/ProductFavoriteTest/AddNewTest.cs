using Application.Services.Products.Favourites.Commands.Create;
using Application.Test.Builders;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.MappingProfile;

namespace Application.Test.ProductFavouriteTest
{
    public class AddNewTest
    {
        [Fact]
        public async Task Handle_Should_Add_Favourite_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            context.Products.Add(new Domain.Products.ProductItem { Name = "test", Price = 12000, Id = 1 });

            var handler = new CreateFavouriteHandler(context);


            var request = new CreateFavouriteCommand(new CreateFavouriteDto { ProductItemId = 1, UserId = "fbcb56c5-e0fd-4e1b-bb12-c6ea78b2657c" });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<CreateFavouriteResponseDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Add a New Favourite Is Success", result.Message.First());
            Assert.NotEmpty(context.ProductItemFavourites.Local);
        }
    }
}
