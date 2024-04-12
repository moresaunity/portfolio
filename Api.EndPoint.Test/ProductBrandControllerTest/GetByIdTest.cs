using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.GetById;
using Application.Services.Products.Brands.Queries.GetById;
using Application.Services.Products.Queries;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.EndPoint.Test.ProductControllerBrandTest
{
    public class GetByIdTest
    {
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
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductBrandController(mediatorMock.Object, mapper, null);

            FindByIdBrandDto dto = new FindByIdBrandDto 
            {
                Id = ValidId,
                Brand = "test"
            };
            var baseDto = new BaseDto<FindByIdBrandDto>(true, new List<string> { "Get By Id Brand Is Success" }, dto);

            mediatorMock.Setup(m => m.Send(It.IsAny<FindByIdBrandRequest>(), default)).ReturnsAsync(baseDto);


            // Act
            var result = controller.Get(ValidId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<BaseDto<ProductBrandGetResultDto>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal(baseDto.Message, returnedDto.Message);
            // ----------------------
            // In Valid Id

            // Arrange
            var invalidbaseDto = new BaseDto<FindByIdBrandDto>(false, new List<string> { "Not Found Brand!" }, null);
            mediatorMock.Setup(m => m.Send(It.IsAny<FindByIdBrandRequest>(), default)).ReturnsAsync(invalidbaseDto);

            // Act
            result = controller.Get(InValidId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var invalidreturnedDto = Assert.IsType<BaseDto<FindByIdBrandDto>>(notFoundResult.Value);
            Assert.False(invalidreturnedDto.IsSuccess);
            Assert.Null(invalidreturnedDto.Data);
            Assert.Equal(invalidbaseDto.Message, invalidreturnedDto.Message);

        }

    }
}
