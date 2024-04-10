using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Brands.Commands.Delete
{
    public class SendCommentHandler : IRequestHandler<DeleteBrandCommand, BaseDto<DeleteBrandResponseDto>>
    {
        private readonly IDataBaseContext context;

        public SendCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<DeleteBrandResponseDto>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = context.ProductBrands.FirstOrDefault(p => p.Id == request.Id);
            if (brand == null) return Task.FromResult(new BaseDto<DeleteBrandResponseDto>(false, new List<string> { "Delete Brand Is Not Success", "Not Found Brand!" }, null));
            var entity = context.ProductBrands.Remove(brand);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<DeleteBrandResponseDto>(true, new List<string> { "Delete Brand Is Success" } ,new DeleteBrandResponseDto
            {
                Id = entity.Entity.Id,
                Brand = brand.Brand
            }));
        }
    }

}
