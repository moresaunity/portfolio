using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ProductItem.Commands.Delete
{
    public class DeleteProductItemCommand : IRequest<BaseDto>
    {
        public int Id { get; set; }
		public DeleteProductItemCommand(int Id)
        {
            this.Id = Id;
        }

    }

}
