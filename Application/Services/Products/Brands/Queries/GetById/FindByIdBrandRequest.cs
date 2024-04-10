using Application.Interfaces.Contexts;
using MediatR;

namespace Application.Services.Products.Brands.Queries.GetById
{
    public class FindByIdBrandRequest : IRequest<FindByIdBrandDto>
    {
        public int Id;

        public FindByIdBrandRequest(int Id)
        {
            this.Id = Id;
        }
    }
    public class FindByIdCommentOfCatalogItemQuery : IRequestHandler<FindByIdBrandRequest, FindByIdBrandDto>
    {
        private readonly IDataBaseContext context;

        public FindByIdCommentOfCatalogItemQuery(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<FindByIdBrandDto> Handle(FindByIdBrandRequest request, CancellationToken cancellationToken)
        {
            var comments = context.ProductBrands?.Select(p => new FindByIdBrandDto
            {
                Id = p.Id,
                Brand = p.Brand
            })?.FirstOrDefault(p => p.Id == request.Id);
            return Task.FromResult(comments);
        }
    }

    public class FindByIdBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}
