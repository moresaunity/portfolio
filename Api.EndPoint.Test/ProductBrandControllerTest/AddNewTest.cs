using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Post;
using Aplication.Services.Products.Brands.Commands.Send;
using Aplication.Services.Products.ProductItem.Commands.Create;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.EndPoint.Test.ProductControllerBrandTest
{
    public class AddNewTest
    {
        [Fact]
        public async Task Handle_Should_Add_Brand_Action_Post_Successfully()
        {
            // Arrange
            MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            IMapper mapper = mockMapper.CreateMapper();
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            ProductBrandController controller = new ProductBrandController(mediatorMock.Object, mapper, null);

            SendBrandResponseDto requestdto = new SendBrandResponseDto 
            {
                Id = 1,
                Brand = "test"
            };
            BaseDto<SendBrandResponseDto> baseDto = new BaseDto<SendBrandResponseDto>(true, new List<string> { "Add a New Brand Is Success" }, requestdto);

            mediatorMock.Setup(m => m.Send(It.IsAny<SendBrandCommand>(), default)).ReturnsAsync(baseDto);

            // Act
            IActionResult result = controller.Post("test");

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            BaseDto<ProductBrandGetResultDto> returnedDto = Assert.IsType<BaseDto<ProductBrandGetResultDto>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal("Add a New Brand Is Success", returnedDto.Message.First());
        }
    }
}
