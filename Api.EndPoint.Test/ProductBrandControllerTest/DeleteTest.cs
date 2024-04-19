using Api.EndPoint.Controllers.V1.Product;
using Api.EndPoint.MappingProfiles;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Aplication.Services.Brands.Commands.Delete;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.EndPoint.Test.ProductControllerBrandTest
{
    public class DeleteTest
    {
        [Fact]
        public async Task Handle_Should_Delete_Brand_Action_Delete_Successfully()
        {
            // Arrange
            MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductEndPointMappingProfile());
            });
            IMapper mapper = mockMapper.CreateMapper();
            Mock<IMediator> mediatorMock = new Mock<IMediator>();
            ProductBrandController controller = new ProductBrandController(mediatorMock.Object, mapper, null);

            DeleteBrandResponseDto dto = new DeleteBrandResponseDto 
            {
                Id = 1,
                Brand = "test"
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBrandCommand>(), default)).ReturnsAsync(new BaseDto<DeleteBrandResponseDto>(true, new List<string> { "Delete Brand Is Success" }, dto));

            // Act
            IActionResult result = controller.Delete(1);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            BaseDto<ProductBrandGetResultDto> returnedDto = Assert.IsType<BaseDto<ProductBrandGetResultDto>>(okResult.Value);
            Assert.True(returnedDto.IsSuccess);
            Assert.Equal("Delete Brand Is Success", returnedDto.Message.First());
        }

    }
}