using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Brands.Commands.Edit
{
    public class EditCommentHandler : IRequestHandler<EditBrandCommand, BaseDto<EditBrandResponseDto>>
    {
        private readonly IDataBaseContext context;

        public EditCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<EditBrandResponseDto>> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            ProductBrand brand = context.ProductBrands.FirstOrDefault(p => request.Id == p.Id);
            if (brand == null) return Task.FromResult(new BaseDto<EditBrandResponseDto>(false, new List<string> { "Add a New Brand Is Not Success" ,"brand Not Found" }, null));
            brand.Brand = request.BrandDto.Brand;
            var entity = context.ProductBrands.Update(brand);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<EditBrandResponseDto>(true, new List<string> { "Edit Brand Is Success" } ,new EditBrandResponseDto
            {
                Id = entity.Entity.Id,
                Brand = brand.Brand
            }));
        }
    }

}
