using Application.Interfaces.Contexts;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.Brands.Commands.Send
{
    public class SendCommentHandler : IRequestHandler<SendBrandCommand, SendBrandResponseDto>
    {
        private readonly IDataBaseContext context;

        public SendCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<SendBrandResponseDto> Handle(SendBrandCommand request, CancellationToken cancellationToken)
        {
            ProductBrand Brand = new ProductBrand
            {
                Brand = request.BrandDto.Brand
            };
            var entity = context.ProductBrands.Add(Brand);
            context.SaveChanges();

            return Task.FromResult(new SendBrandResponseDto
            {
                Id = entity.Entity.Id,
                Brand = Brand.Brand
            });
        }
    }

}
