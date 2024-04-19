using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.Favourites.Commands.Delete
{
    public class DeleteFavouriteHandler : IRequestHandler<DeleteFavouriteCommand, BaseDto>
    {
        private readonly IDataBaseContext context;

        public DeleteFavouriteHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto> Handle(DeleteFavouriteCommand request, CancellationToken cancellationToken)
        {
            var favourite = context.ProductItemFavourites.FirstOrDefault(p => p.Id == request.Id);
            if (favourite == null) favourite = context.ProductItemFavourites.Local.FirstOrDefault(p => p.Id == request.Id);
            if (favourite == null) return Task.FromResult(new BaseDto(false, new List<string> { "Delete Favourite Is Not Success", "Not Found Favourite!" }));
            context.ProductItemFavourites.Remove(favourite);
            if(context.ProductBrands.Count() != 0) context.SaveChanges();

            return Task.FromResult(new BaseDto(true, new List<string> { "Delete Favourite Is Success" }));
        }
    }

}
