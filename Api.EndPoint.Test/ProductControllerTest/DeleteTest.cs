using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Aplication.Services.Products.ProductItem.Commands.Delete;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.EndPoint.Test.ProductControllerTest
{
    public class DeleteTest
    {
        [Fact]
        public async Task Handle_Should_Delete_Product_Action_Delete_Successfully()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mapper, mediatorMock.Object, null);

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductItemCommand>(), default)).ReturnsAsync(new BaseDto(true, new List<string> { "Delete Product Is Success" }));

            // Act
            var result = controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal("Delete Product Is Success", returnedDto.Message.First());
        }

    }
}