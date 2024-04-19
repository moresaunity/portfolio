using Domain.Dtos;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Products.Favourites.Commands.Delete
{
    public class DeleteFavouriteCommand : IRequest<BaseDto>
    {
        public int Id { get; set; }
        public DeleteFavouriteCommand(int Id)
        {
            this.Id = Id;
        }

    }

}
