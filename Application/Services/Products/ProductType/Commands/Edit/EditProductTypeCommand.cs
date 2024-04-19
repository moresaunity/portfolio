using Aplication.Services.Products.ProductType.Commands.Edit;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ProductType.Commands.Edit
{
    public class EditProductTypeCommand : IRequest<BaseDto<EditProductTypeResponseDto>>
    {
        public EditProductTypeDto ProductTypeDto { get; set; }
        public int Id { get; set; }
        public EditProductTypeCommand(EditProductTypeDto contactDto, int Id)
        {
            ProductTypeDto = contactDto;
            this.Id = Id;
        }

    }

}
