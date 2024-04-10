using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;

namespace Application.Services.Products.Brands.Queries.GetById
{
    public class FindByIdBrandRequest : IRequest<BaseDto<FindByIdBrandDto>>
    {
        public int Id;

        public FindByIdBrandRequest(int Id)
        {
            this.Id = Id;
        }
    }
    public class FindByIdCommentOfCatalogItemQuery : IRequestHandler<FindByIdBrandRequest, BaseDto<FindByIdBrandDto>>
    {
        private readonly IDataBaseContext context;

        public FindByIdCommentOfCatalogItemQuery(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<FindByIdBrandDto>> Handle(FindByIdBrandRequest request, CancellationToken cancellationToken)
        {
            var brand = context.ProductBrands?.Select(p => new FindByIdBrandDto
            {
                Id = p.Id,
                Brand = p.Brand
            })?.FirstOrDefault(p => p.Id == request.Id);
            if (brand == null) return Task.FromResult(new BaseDto<FindByIdBrandDto>(false, new List<string> { "Not Found Brand!" }, null));
            return Task.FromResult(new BaseDto<FindByIdBrandDto>(true, new List<string> { "Get By Id Brand Is Success" }, brand));
        }
    }

    public class FindByIdBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}
