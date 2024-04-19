using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ProductType.Commands.Edit
{
    public class EditCommentHandler : IRequestHandler<EditProductTypeCommand, BaseDto<EditProductTypeResponseDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public EditCommentHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<BaseDto<EditProductTypeResponseDto>> Handle(EditProductTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Products.ProductType? productType = context.ProductTypes.FirstOrDefault(p => p.Id == request.Id);
            if (productType == null) productType = context.ProductTypes.Local.FirstOrDefault(p => p.Id == request.Id);
            if (productType == null) return Task.FromResult(new BaseDto<EditProductTypeResponseDto>(false, new List<string> { "Edit Product Type Is Not Success", "Product Type Is Not Found" }, null));
            productType.Type = request.ProductTypeDto.ProductType;
            productType.ParentProductTypeId = request.ProductTypeDto.ParentProductTypeId;
            
            context.ProductTypes.Update(productType);
            if (context.ProductTypes.Count() != 0) context.SaveChanges();

            EditProductTypeResponseDto result = mapper.Map<EditProductTypeResponseDto>(productType);

            return Task.FromResult(new BaseDto<EditProductTypeResponseDto>(true, new List<string> { "Edit Product Type Is Success" }, result));
        }
    }

}
