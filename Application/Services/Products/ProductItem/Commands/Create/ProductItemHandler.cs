using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ProductItem.Commands.Create
{
    public class SendCommentHandler : IRequestHandler<CreateProductItemCommand, BaseDto<int>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public SendCommentHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<int>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = mapper.Map<Domain.Products.ProductItem>(request.ProductDto);
            catalogItem.ProductItemImages = new List<ProductItemImage>();
            if (request.ProductDto.Images != null)
            {
                foreach (var item in request.ProductDto.Images)
                {
                    catalogItem.ProductItemImages.Add(new ProductItemImage { Src = item.Src });
                }
            }

            if (catalogItem.Slug == null)
            {
                catalogItem.Slug = GenerateSlug(catalogItem.Name);
            }
            var entity = context.Products.Add(catalogItem);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<int>(true, new List<string> { "Add Product Is Success" }, entity.Entity.Id));
        }
        static string GenerateSlug(string input)
        {
            string slug = input.ToLower(); // Convert to lowercase
            //slug = Regex.Replace(slug, @"[^a-z0-9\s-]", ""); // Remove non-alphanumeric characters
            //slug = Regex.Replace(slug, @"\s+", " ").Trim(); // Replace multiple spaces with a single space
            slug = slug.Replace(" ", "-"); // Replace spaces with hyphens
            return slug;
        }
    }

}
