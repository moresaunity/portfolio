using Aplication.Services.Products.ProductItem.Commands.Create;
using Application.Test.Builders;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.MappingProfile;

namespace Application.Test.ProductItemTest
{
    public class AddNewTest
    {
        [Fact]
        public async Task Handle_Should_Add_Product_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new SendCommentHandler(context, mapper);

            var request = new CreateProductItemCommand(new CreateProductItemDto
            {
                Name = "Sample Product",
                Description = "description",
                AvailableStock = 12,
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<int>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Add Product Is Success", result.Message.First());
            Assert.Equal(1, result.Data); // Assuming the entity ID is 1
        }
    }
}
