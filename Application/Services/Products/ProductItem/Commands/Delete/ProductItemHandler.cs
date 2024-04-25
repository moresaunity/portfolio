using Application.Exceptions;
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
            if (product == null) product = context.Products.Local.FirstOrDefault(p => p.Id == request.Id);
            if (product == null) return Task.FromException<BaseDto>(new NotFoundException(nameof(product), request.Id));

            context.Products.Remove(product);
            if(context.Products.Count() != 0) context.SaveChanges();

            return Task.FromResult(new BaseDto(true, new List<string> { "Delete Product Is Success" }));
        }
    }
}
