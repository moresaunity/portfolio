using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;

namespace Application.Services.Products.Brands.Queries.Get
{
    public class GetBrandRequest : IRequest<BaseDto<List<GetBrandDto>>>
    {

    }
    public class GetCommentOfCatalogItemQuery : IRequestHandler<GetBrandRequest, BaseDto<List<GetBrandDto>>>
    {
        private readonly IDataBaseContext context;

        public GetCommentOfCatalogItemQuery(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<List<GetBrandDto>>> Handle(GetBrandRequest request, CancellationToken cancellationToken)
        {
            var Brand = context.ProductBrands?.Select(p => new GetBrandDto
            {
                Id = p.Id,
                Brand = p.Brand
            })?.ToList();
            if (Brand.Count == 0) Brand = context.ProductBrands?.Local.Select(p => new GetBrandDto
            {
                Id = p.Id,
                Brand = p.Brand
            })?.ToList();

            if (Brand == null) return Task.FromResult(new BaseDto<List<GetBrandDto>>(false, new List<string> { "Brand Is Empty" }, null));
            return Task.FromResult(new BaseDto<List<GetBrandDto>>(true, new List<string> { "Get Brands Is Success" }, Brand));
        }
    }

    public class GetBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}
