using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aplication.Services.ProductType.Commands.Delete
{
    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, BaseDto>
    {
        private readonly IDataBaseContext context;

        public DeleteProductTypeHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Products.ProductType? ProductType = context.ProductTypes.FirstOrDefault(p => p.Id == request.Id);
            if (ProductType == null) ProductType = context.ProductTypes.Local.FirstOrDefault(p => p.Id == request.Id);
			if (ProductType == null) return Task.FromResult(new BaseDto(false, new List<string> { "Delete ProductType Is Not Success", "Not Found ProductType!" }));
            EntityEntry<Domain.Products.ProductType> entity = context.ProductTypes.Remove(ProductType);
            if(context.ProductTypes.Count() != 0) context.SaveChanges();

            return Task.FromResult(new BaseDto(true, new List<string> { "Delete ProductType Is Success" }));
        }
    }

}
