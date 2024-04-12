using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Post;
using Aplication.Services.Products.ProductItem.Commands.Create;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.EndPoint.Test.ProductControllerTest
{
    public class AddNewTest
    {
        [Fact]
        public async Task Handle_Should_Add_Product_Action_Post_Successfully()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mapper, mediatorMock.Object, null);

            var baseDto = new BaseDto<int>(true, new List<string> { "Add Product Is Success" }, 1);

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductItemCommand>(), default)).ReturnsAsync(baseDto);

            ProductItemPostRequestDto request = new ProductItemPostRequestDto 
            {
                Name = "name",
                Price = 12000
            };

            // Act
            var result = controller.Post(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<int>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal("Add Product Is Success", returnedDto.Message.First());
        }
    }
}
