using Aplication.Services.Brands.Commands.Edit;
using Application.Test.Builders;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using Infrastructure.MappingProfile;
using Persistance.Contexts;

namespace Application.Test.ProductBrandTest
{
    public class EditTest
    {
        [Fact]
        public async Task Handle_Should_Edit_Brand_Successfully()
        {
            // Arrange
            var databaseBuilder = new DataBaseBuilder();
            var context = databaseBuilder.GetDbContext();
			await AddBrand(context);
			var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var handler = new EditCommentHandler(context);

            var request = new EditBrandCommand(new EditBrandDto
            {
				Brand = "Etest"
			}, 1);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BaseDto<EditBrandResponseDto>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Edit Brand Is Success", result.Message.First());
			Assert.Equal(request.BrandDto.Brand, result.Data.Brand);
            Assert.Equal(1, result.Data.Id); // Assuming the entity ID is 1
        }
		private async Task AddBrand(DataBaseContext context)
		{
			context.ProductBrands.Add(new ProductBrand
			{
				Id = 1,
				Brand = "test"
			});
		}
	}
}
