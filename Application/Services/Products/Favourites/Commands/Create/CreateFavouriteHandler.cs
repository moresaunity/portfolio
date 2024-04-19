using Application.Interfaces.Contexts;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Application.Services.Products.Favourites.Commands.Create
{
    public class CreateFavouriteHandler : IRequestHandler<CreateFavouriteCommand, BaseDto<CreateFavouriteResponseDto>>
    {
        private readonly IDataBaseContext context;

        public CreateFavouriteHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<CreateFavouriteResponseDto>> Handle(CreateFavouriteCommand request, CancellationToken cancellationToken)
        {
            var productItem = context.Products.FirstOrDefault(p => p.Id == request.FavouriteDto.ProductItemId);
            if (productItem == null) productItem = context.Products.Local.FirstOrDefault(p => p.Id == request.FavouriteDto.ProductItemId);
            if (productItem == null) return Task.FromResult(new BaseDto<CreateFavouriteResponseDto>(false, new List<string> { "Add a New Favorite Is Not Success", "Product Is Not Found" }, null));
            ProductItemFavourite favourite = new ProductItemFavourite
            {
                ProductItemId = productItem.Id,
                UserId = request.FavouriteDto.UserId
            };
            var entity = context.ProductItemFavourites.Add(favourite);
            if(context.ProductItemFavourites.Count() != 0) context.SaveChanges();
            return Task.FromResult(new BaseDto<CreateFavouriteResponseDto>(true, new List<string> { "Add a New Favourite Is Success" }, new CreateFavouriteResponseDto { Id = entity.Entity.Id }));
        }
    }

}
