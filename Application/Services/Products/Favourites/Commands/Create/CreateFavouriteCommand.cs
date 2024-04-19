using Domain.Dtos;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.Favourites.Commands.Create
{
    public class CreateFavouriteCommand : IRequest<BaseDto<CreateFavouriteResponseDto>>
    {
        public CreateFavouriteDto FavouriteDto { get; set; }
        public CreateFavouriteCommand(CreateFavouriteDto FavouriteDto)
        {
            this.FavouriteDto = FavouriteDto;
        }

    }

}
