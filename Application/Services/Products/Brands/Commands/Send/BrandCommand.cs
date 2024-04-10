using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.Brands.Commands.Send
{
    public class SendBrandCommand : IRequest<BaseDto<SendBrandResponseDto>>
    {
        public SendBrandDto BrandDto { get; set; }
        public SendBrandCommand(SendBrandDto contactDto)
        {
            BrandDto = contactDto;
        }

    }

}
