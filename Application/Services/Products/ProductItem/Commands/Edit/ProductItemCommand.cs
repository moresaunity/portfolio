using Application.Services.Products.ProductItem.Commands.Edit;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ProductItem.Commands.Edit
{
    public class EditProductItemCommand : IRequest<BaseDto<EditProductItemResultDto>>
    {
        public EditProductItemDto ProductDto { get; set; }
        public int Id { get; set; }
        public EditProductItemCommand(EditProductItemDto ProductDto, int Id)
        {
            this.ProductDto = ProductDto;
            this.Id = Id;
        }

    }

}
