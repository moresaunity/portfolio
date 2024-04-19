using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ProductType.Commands.Send
{
    public class SendProductTypeCommand : IRequest<BaseDto<SendProductTypeResponseDto>>
    {
        public SendProductTypeDto ProductTypeDto { get; set; }
        public SendProductTypeCommand(SendProductTypeDto contactDto)
        {
            ProductTypeDto = contactDto;
        }

    }

}
