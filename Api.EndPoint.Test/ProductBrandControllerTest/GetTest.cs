using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.GetById;
using Application.Services.Products.Brands.Queries.Get;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.MappingProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Api.EndPoint.Test.ProductControllerBrandTest
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
            var controller = new ProductBrandController(mediatorMock.Object, mapper, null);

            var Dto = new List<GetBrandDto>
            {
                new GetBrandDto
                {
                    Id = 1,
                    Brand = "test",
                }
            };
            var baseDto = new BaseDto<List<GetBrandDto>>(true, new List<string> { "Get Brands Is Success" }, Dto);

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBrandRequest>(), default)).ReturnsAsync(baseDto);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<List<ProductBrandGetResultDto>>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal(baseDto.Message, returnedDto.Message);
            Assert.Equal(Dto.First().Id, returnedDto.Data.First().Id);
        }
    }
}
