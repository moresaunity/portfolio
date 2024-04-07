using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Brands.Commands.Delete
{
    public class DeleteBrandCommand : IRequest<DeleteBrandResponseDto>
    {
        public int Id { get; set; }
        public DeleteBrandCommand(int Id)
        {
            this.Id = Id;
        }

    }

}
