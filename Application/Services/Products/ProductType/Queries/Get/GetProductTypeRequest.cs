using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products.ProductType.Queries.Get
{
    public class GetProductTypeRequest : IRequest<BaseDto<PaginatedItemsDto<GetProductTypeDto>>>
    {
        public GetProductTypeRequestDto requestDto { get; }
        public GetProductTypeRequest(GetProductTypeRequestDto request)
        {
            requestDto = request;
        }
    }
    public class GetCommentOfProductTypeQuery : IRequestHandler<GetProductTypeRequest, BaseDto<PaginatedItemsDto<GetProductTypeDto>>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetCommentOfProductTypeQuery(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<PaginatedItemsDto<GetProductTypeDto>>> Handle(GetProductTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Products.ProductType> model = context.ProductTypes
                .OrderByDescending(p => p.Id).AsQueryable();

            if (model.Count() == 0) model = context.ProductTypes.Local
                .OrderByDescending(p => p.Id).AsQueryable();

            int rowCount = 0;

            List<Domain.Products.ProductType> data = model.Include(p => p.ParentProductType).Include(p => p.SubType).Where(p => p.ParentProductTypeId == request.requestDto.ParentId).PagedResult(request.requestDto.Page, request.requestDto.PageSize, out rowCount).ToList();
            foreach (var item in data) if (item.ParentProductType != null) { item.ParentProductType.SubType = null; }
            List<GetProductTypeDto> result = mapper.Map<List<GetProductTypeDto>>(data);

            return Task.FromResult(new BaseDto<PaginatedItemsDto<GetProductTypeDto>>(true, new List<string> { "Get Product Types Is Success" }, new PaginatedItemsDto<GetProductTypeDto>(request.requestDto.Page, request.requestDto.PageSize, rowCount, result)));
        }
    }
    public class GetProductTypeRequestDto
    {
        public int? ParentId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
    public class GetProductTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentProductTypeId { get; set; }
        public Domain.Products.ProductType ParentProductType { get; set; }
        public ICollection<Domain.Products.ProductType> SubType { get; set; }
    }
}
