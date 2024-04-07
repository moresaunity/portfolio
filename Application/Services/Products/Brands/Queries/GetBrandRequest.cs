using Application.Interfaces.Contexts;
using MediatR;

namespace Aplication.Services.Brands.Queries
{
    public class GetBrandRequest : IRequest<List<GetBrandDto>>
    {

    }
    public class GetCommentOfCatalogItemQuery : IRequestHandler<GetBrandRequest, List<GetBrandDto>>
    {
        private readonly IDataBaseContext context;

        public GetCommentOfCatalogItemQuery(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<List<GetBrandDto>> Handle(GetBrandRequest request, CancellationToken cancellationToken)
        {
            var Brand = context.ProductBrands?.Select(p => new GetBrandDto
            {
                Id = p.Id,
                Brand = p.Brand
            })?.ToList();
            return Task.FromResult(Brand);
        }
    }

    public class GetBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}
