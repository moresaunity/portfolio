using Domain.Dtos;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.ProductType.Commands.Delete
{
    public class DeleteProductTypeCommand : IRequest<BaseDto>
    {
        public int Id { get; set; }
        public DeleteProductTypeCommand(int Id)
        {
            this.Id = Id;
        }

    }

}
