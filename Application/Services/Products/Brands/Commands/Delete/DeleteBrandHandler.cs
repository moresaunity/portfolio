using Application.Interfaces.Contexts;
using MediatR;

namespace Aplication.Services.Brands.Commands.Delete
{
    public class SendCommentHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponseDto>
    {
        private readonly IDataBaseContext context;

        public SendCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<DeleteBrandResponseDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var contact = context.ProductBrands.FirstOrDefault(p => p.Id == request.Id);
            var entity = context.ProductBrands.Remove(contact);
            context.SaveChanges();

            return Task.FromResult(new DeleteBrandResponseDto
            {
                Id = entity.Entity.Id,
                Brand = contact.Brand
            });
        }
    }

}
