using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Aplication.Services.Brands.Commands.Edit;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.EndPoint.Test.ProductControllerBrandTest
{
    public class EditTest
    {
        [Fact]
        public async Task Handle_Should_Edit_Product_Action_Put_Successfully()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductBrandController(mediatorMock.Object, mapper, null);

            EditBrandResponseDto dto = new EditBrandResponseDto
            {
                Id = 1,
                Brand = "test",
            };
            var baseDto = new BaseDto<EditBrandResponseDto>(true, new List<string> { "Edit Brand Is Success" }, dto);

            mediatorMock.Setup(m => m.Send(It.IsAny<EditBrandCommand>(), default)).ReturnsAsync(baseDto);

            // Act
            var result = controller.Put(1, "test");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<ProductBrandGetResultDto>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal("Edit Brand Is Success", returnedDto.Message.First());
        }

    }
}