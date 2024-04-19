using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
using Domain.Dtos;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products.ProductType.Queries.Get
{
    public class GetProductTypeByIdRequest : IRequest<BaseDto<GetProductTypeByIdDto>>
    {
        public int Id { get; }
        public GetProductTypeByIdRequest(int Id)
        {
            this.Id = Id;
        }
    }
    public class GetCommentOfProductTypeByIdQuery : IRequestHandler<GetProductTypeByIdRequest, BaseDto<GetProductTypeByIdDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetCommentOfProductTypeByIdQuery(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<GetProductTypeByIdDto>> Handle(GetProductTypeByIdRequest request, CancellationToken cancellationToken)
        {
            Domain.Products.ProductType? productType = context.ProductTypes.Include(p => p.ParentProductType).Include(p => p.SubType).FirstOrDefault(p => p.Id == request.Id);
            productType.ParentProductType.SubType = null;
            if (productType == null) productType = context.ProductTypes.Local.FirstOrDefault(p => p.Id == request.Id);
            if (productType == null) return Task.FromResult(new BaseDto<GetProductTypeByIdDto>(false, new List<string> { "Get Product Type By Id Is Not Success", "Product Type Is Not Found" }, null));
            GetProductTypeByIdDto? result = mapper.Map<GetProductTypeByIdDto>(productType);
            if (result == null) return Task.FromResult(new BaseDto<GetProductTypeByIdDto>(false, new List<string> { "Get Product Type By Id Is Not Success", "Map Product Type Is Not Success" }, null));
            return Task.FromResult(new BaseDto<GetProductTypeByIdDto>(true, new List<string> { "Get Product Type By Id Is Success" }, result));
        }
    }
    public class GetProductTypeByIdDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentProductTypeId { get; set; }
        public Domain.Products.ProductType ParentProductType { get; set; }
        public ICollection<Domain.Products.ProductType> SubType { get; set; }
    }
}
