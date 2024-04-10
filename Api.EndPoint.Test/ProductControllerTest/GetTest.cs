using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.GetById;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.MappingProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Api.EndPoint.Test.ProductControllerTest
{
    public class GetTest
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WhenCacheIsNotAvailable()
        {
            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mapper, mediatorMock.Object, null);

            var requestDto = new ProductItemGetRequestDto();
            var productItemDtoList = new List<GetProductItemDto>
            {
                new GetProductItemDto
                {
                    Id = 1,
                    Name = "test",
                    Description = "test"
                }
            };
            var baseDto = new BaseDto<List<GetProductItemDto>>(true, new List<string> { "Success" }, productItemDtoList);

            mediatorMock.Setup(m => m.Send(It.IsAny<GetProductItemRequest>(), default)).ReturnsAsync(baseDto);

            // Act
            var result = controller.Get(requestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<List<ProductItemGetResultDto>>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal(productItemDtoList.First().Id, returnedDto.Data.First().Id);
        }
        [Theory]
        [InlineData(1, -1)]
        public async Task GetById_ReturnsOkResult_WhenCacheIsNotAvailable(int ValidId, int InValidId)
        {
            // Valid Id

            // Arrange
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var cacheMock = new Mock<IMemoryCache>();
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mapper, mediatorMock.Object, cacheMock.Object);

            var getByIdProductItemDto = new GetByIdProductItemDto();
            var baseDto = new BaseDto<GetByIdProductItemDto>(true, new List<string> { "Success" }, getByIdProductItemDto);

            mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdProductItemRequest>(), default)).ReturnsAsync(baseDto);


            // Act
            var result = controller.Get(ValidId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<ProductItemGetByIdResultDto>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);

            // ----------------------
            // In Valid Id

            // Arrange
            var InValidgetByIdProductItemDto = new GetByIdProductItemDto();
            var InValidbaseDto = new BaseDto<GetByIdProductItemDto>(false, new List<string> { "Product Is NotFound" }, getByIdProductItemDto);
            mediatorMock.Setup(m => m.Send(It.IsAny<GetByIdProductItemRequest>(), default)).ReturnsAsync(InValidbaseDto);

            // Act
            result = controller.Get(InValidId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
