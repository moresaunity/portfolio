using Application.Test.Builders;
using AutoMapper;
using Infrastructure.MappingProfile;
using Domain.Dtos;
using Persistance.Contexts;
using Application.Services.Products.Favourites.Queries;
using Domain.Products;

namespace Application.Test.ProductFavouriteTest
{
    public class GetListTest
    {
        [Fact]
        public async Task Handle_Should_GetAll_Favourite_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new GetFavouriteHandler(context, mapper);

            await AddFavourite(context);

            // Act
            GetFavouriteRequest request = new GetFavouriteRequest(new GetFavouriteCommand());
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<PaginatedItemsDto<GetFavouriteResponseDto>>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Get Favourites Is Success", result.Message.First());
        }
        private async Task AddFavourite(DataBaseContext context)
        {
            context.Products.Add(new ProductItem
            {
                Id = 1,
                Name = "Test",
                Price = 12000
            });
            context.ProductItemFavourites.Add(new ProductItemFavourite
            {
                Id = 1,
                ProductItemId = 1,
                UserId = "fbcb56c5-e0fd-4e1b-bb12-c6ea78b2657c"
            });
            context.ProductItemFavourites.Add(new ProductItemFavourite
            {
                Id = 2,
                ProductItemId = 1,
                UserId = "fbcb56c5-e0fd-4e1b-bb12-c6ea78b2657c"
            });
        }
    }
}
