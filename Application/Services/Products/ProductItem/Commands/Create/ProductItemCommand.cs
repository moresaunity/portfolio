using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ProductItem.Commands.Create
{
    public class CreateProductItemCommand : IRequest<BaseDto<int>>
    {
        public CreateProductItemDto ProductDto { get; set; }
        public CreateProductItemCommand(CreateProductItemDto ProductDto)
        {
            this.ProductDto = ProductDto;
        }

    }

}
