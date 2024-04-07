using Domain.Dtos;
using Application.Interfaces.Contexts;
using Application.Services.UriComposer;
using MediatR;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products.Favourites.Queries
{
    public class GetFavouriteForUserRequest : IRequest<PaginatedItemsDto<GetFavouriteForUserResponseDto>>
    {
        public GetFavouriteForUserCommand favouriteDto;

        public GetFavouriteForUserRequest(GetFavouriteForUserCommand FavouriteDto)
        {
            favouriteDto = FavouriteDto;
        }
    }
    public class GetFavouriteForUserHandler : IRequestHandler<GetFavouriteForUserRequest, PaginatedItemsDto<GetFavouriteForUserResponseDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IUriComposerService uriComposerService;

        public GetFavouriteForUserHandler(IDataBaseContext context, IUriComposerService uriComposerService)
        {
            this.context = context;
            this.uriComposerService = uriComposerService;
        }
        public Task<PaginatedItemsDto<GetFavouriteForUserResponseDto>> Handle(GetFavouriteForUserRequest request, CancellationToken cancellationToken)
        {
            var model = context.Products.Include(p => p.ProductItemImages).Include(p => p.Discounts).Include(p => p.ProductItemFavourites)
                .Where(p => p.ProductItemFavourites.Any(f => f.UserId == request.favouriteDto.UserId)).OrderByDescending(p => p.Id).AsQueryable();

            int rowCount = 0;
            var data = model.PagedResult(request.favouriteDto.Page, request.favouriteDto.PageSize, out rowCount).ToList().Select(p => new GetFavouriteForUserResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Rate = 4,
                AvailableStock = p.AvailableStock,
                Image = p.ProductItemImages.FirstOrDefault() != null ? uriComposerService.ComposeImageUri(p.ProductItemImages.FirstOrDefault().Src) : uriComposerService.ComposeImageUri("/Resources/images/empty.jpg")
            }).ToList();

            return Task.FromResult(new PaginatedItemsDto<GetFavouriteForUserResponseDto>(request.favouriteDto.Page, request.favouriteDto.PageSize, rowCount, data));
        }
    }
    public class GetFavouriteForUserCommand
    {
        public string UserId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
    public class GetFavouriteForUserResponseDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public int AvailableStock { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
