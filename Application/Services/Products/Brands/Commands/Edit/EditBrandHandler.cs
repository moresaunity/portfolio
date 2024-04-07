using Application.Interfaces.Contexts;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Brands.Commands.Edit
{
    public class EditCommentHandler : IRequestHandler<EditBrandCommand, EditBrandResponseDto>
    {
        private readonly IDataBaseContext context;

        public EditCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<EditBrandResponseDto> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            ProductBrand Brand = new ProductBrand
            {
                Brand = request.BrandDto.Brand
            };
            var entity = context.ProductBrands.Add(Brand);
            context.SaveChanges();

            return Task.FromResult(new EditBrandResponseDto
            {
                Id = entity.Entity.Id,
                Brand = Brand.Brand
            });
        }
    }

}
