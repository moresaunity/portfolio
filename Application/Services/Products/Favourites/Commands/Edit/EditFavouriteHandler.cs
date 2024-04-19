using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.Favourites.Commands.Edit
{
    public class EditCommentHandler : IRequestHandler<EditFavouriteCommand, BaseDto<EditFavouriteResponseDto>>
    {
        private readonly IDataBaseContext context;

        public EditCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<EditFavouriteResponseDto>> Handle(EditFavouriteCommand request, CancellationToken cancellationToken)
        {
            ProductItemFavourite favourite = context.ProductItemFavourites.FirstOrDefault(p => request.Id == p.Id);
            if (favourite == null) favourite = context.ProductItemFavourites.Local.FirstOrDefault(p => request.Id == p.Id);
            if (favourite == null) return Task.FromResult(new BaseDto<EditFavouriteResponseDto>(false, new List<string> { "Edit Favourite Is Not Success" ,"Favourite Not Found" }, null));
            favourite.UserId = request.FavouriteDto.UserId;
            favourite.ProductItemId = request.FavouriteDto.ProductId;
            var entity = context.ProductItemFavourites.Update(favourite);
            if(context.ProductItemFavourites.Count() != 0) context.SaveChanges();

            return Task.FromResult(new BaseDto<EditFavouriteResponseDto>(true, new List<string> { "Edit Favourite Is Success" } ,new EditFavouriteResponseDto
            {
                Id = entity.Entity.Id,
                ProductId = entity.Entity.ProductItemId,
                UserId = entity.Entity.UserId
            }));
        }
    }

}
