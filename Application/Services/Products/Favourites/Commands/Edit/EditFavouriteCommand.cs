using Domain.Dtos;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Products.Favourites.Commands.Edit
{
    public class EditFavouriteCommand : IRequest<BaseDto<EditFavouriteResponseDto>>
    {
        public EditFavouriteDto FavouriteDto { get; set; }
        public int Id { get; set; }
        public EditFavouriteCommand(EditFavouriteDto FavouriteDto, int Id)
        {
            this.FavouriteDto = FavouriteDto;
            this.Id = Id;
        }

    }

}
