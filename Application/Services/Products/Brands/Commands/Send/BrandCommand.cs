using MediatR;

namespace Aplication.Services.Products.Brands.Commands.Send
{
    public class SendBrandCommand : IRequest<SendBrandResponseDto>
    {
        public SendBrandDto BrandDto { get; set; }
        public SendBrandCommand(SendBrandDto contactDto)
        {
            BrandDto = contactDto;
        }

    }

}
