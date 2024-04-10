using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.Brands.Commands.Send
{
    public class SendCommentHandler : IRequestHandler<SendBrandCommand, BaseDto<SendBrandResponseDto>>
    {
        private readonly IDataBaseContext context;

        public SendCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<SendBrandResponseDto>> Handle(SendBrandCommand request, CancellationToken cancellationToken)
        {
            ProductBrand Brand = new ProductBrand
            {
                Brand = request.BrandDto.Brand
            };
            var entity = context.ProductBrands.Add(Brand);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<SendBrandResponseDto>(true, new List<string> { "Add a New Brand Is Success" }, new SendBrandResponseDto
            {
                Id = entity.Entity.Id,
                Brand = Brand.Brand
            }));
        }
    }

}
