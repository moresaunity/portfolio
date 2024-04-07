using Application.Interfaces.Contexts;
using Application.Services.Products.ProductItem;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products.Queries
{
    public class GetByIdProductItemRequest: IRequest<BaseDto<GetByIdProductItemDto>>
    {
        public int Id { get; set; }

        public GetByIdProductItemRequest(int Id)
        {
            this.Id = Id;
        }
    }
    public class GetByIdCommentOfProductItemQuery : IRequestHandler<GetByIdProductItemRequest, BaseDto<GetByIdProductItemDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetByIdCommentOfProductItemQuery(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<GetByIdProductItemDto>> Handle(GetByIdProductItemRequest request, CancellationToken cancellationToken)
        {
            var product = context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductItemImages)
                .Include(p => p.Discounts)
                .FirstOrDefault(p => p.Id == request.Id);
            if (product == null) return Task.FromResult(new BaseDto<GetByIdProductItemDto>(false, new List<string> { "Product Is NotFound" }, null));

            GetByIdProductItemDto result = mapper.Map<GetByIdProductItemDto>(product);

            return Task.FromResult(new BaseDto<GetByIdProductItemDto>(true, new List<string> { "Get By Id Product Is Success" }, result));
        }
    }
    public class GetByIdProductItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Slug { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public List<ProductItemFeature_dto> Features { get; set; }
        public List<ProductItemImage_Dto> Images { get; set; }
    }
}
