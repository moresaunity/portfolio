using Aplication.Services.Products.Brands.Commands.Send;
using Application.Test.Builders;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.MappingProfile;

namespace Application.Test.ProductBrandTest
{
    public class AddNewTest
    {
        [Fact]
        public async Task Handle_Should_Add_Brand_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new SendCommentHandler(context);

            var request = new SendBrandCommand(new SendBrandDto{Brand = "test"});

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<SendBrandResponseDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Add a New Brand Is Success", result.Message.First());
            Assert.NotEmpty(context.ProductBrands.Local);
        }
    }
}
