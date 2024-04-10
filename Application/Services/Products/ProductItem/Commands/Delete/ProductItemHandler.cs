using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ProductItem.Commands.Delete
{
    public class DeleteProductCommentHandler : IRequestHandler<DeleteProductItemCommand, BaseDto>
    {
        private readonly IDataBaseContext context;

        public DeleteProductCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto> Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == request.Id);
            if (product == null) return Task.FromResult(new BaseDto(false, new List<string> { "product is not found" }));

            context.Products.Remove(product);
            context.SaveChanges();

            return Task.FromResult(new BaseDto(true, new List<string> { "Delete Product Is Success" }));
        }
    }
}
