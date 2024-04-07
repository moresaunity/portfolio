using Application.Interfaces.Contexts;
using Domain.Products;
using MediatR;

namespace Application.Services.Products.Favourites.Commands.Create
{
    public class EditCommentHandler : IRequestHandler<CreateFavouriteCommand, CreateFavouriteResponseDto>
    {
        private readonly IDataBaseContext context;

        public EditCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<CreateFavouriteResponseDto> Handle(CreateFavouriteCommand request, CancellationToken cancellationToken)
        {
            var productItem = context.Products.FirstOrDefault(p => p.Id == request.FavouriteDto.ProductItemId);
            ProductItemFavourite favourite = new ProductItemFavourite
            {
                ProductItemId = productItem.Id,
                UserId = request.FavouriteDto.UserId
            };
            var entity = context.ProductItemFavourites.Add(favourite);
            context.SaveChanges();

            return Task.FromResult(new CreateFavouriteResponseDto
            {
                Status = true,
                Message = "Favourite is Added!",
                Id = entity.Entity.Id
            });
        }
    }

}
