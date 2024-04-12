using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Put;
using Aplication.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.ProductItem.Commands.Edit;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.EndPoint.Test.ProductControllerTest
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
            var controller = new ProductController(mapper, mediatorMock.Object, null);

            EditProductItemResultDto dto = new EditProductItemResultDto 
            {
                Id = 1,
                Name = "name",
                Price = 12000
            };
            var baseDto = new BaseDto<EditProductItemResultDto>(true, new List<string> { "Edit Product Is Success" }, dto);

            mediatorMock.Setup(m => m.Send(It.IsAny<EditProductItemCommand>(), default)).ReturnsAsync(baseDto);

            ProductItemPutRequestDto request = new ProductItemPutRequestDto
            {
                Name = "name",
                Price = 12000
            };

            // Act
            var result = controller.Put(1, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<ProductItemPutResultDto>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal("Edit Product Is Success", returnedDto.Message.First());
        }

    }
}