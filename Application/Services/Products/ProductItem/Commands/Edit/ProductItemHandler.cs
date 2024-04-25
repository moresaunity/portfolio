using Application.Exceptions;
using Application.Interfaces.Contexts;
using Application.Services.Products.ProductItem.Commands.Edit;
using AutoMapper;
using Domain.Dtos;
using Domain.Products;
using MediatR;

namespace Aplication.Services.Products.ProductItem.Commands.Edit
{
    public class EditProductCommentHandler : IRequestHandler<EditProductItemCommand, BaseDto<EditProductItemResultDto>>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public EditProductCommentHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
		public Task<BaseDto<EditProductItemResultDto>> Handle(EditProductItemCommand request, CancellationToken cancellationToken)
		{
			var product = context.Products.FirstOrDefault(p => p.Id == request.Id);
			if (product == null) product = context.Products.Local.FirstOrDefault(p => p.Id == request.Id);

			product = mapper.Map(request.ProductDto, product);
			product.Id = request.Id;
			var entity = context.Products.Update(product);
			Domain.Products.ProductItem productResult;
			if (context.Products.Local.Count == 0)
			{
				context.SaveChanges();
				productResult = context.Products.FirstOrDefault(p => p.Id == entity.Entity.Id);
			} else productResult = context.Products.Local.FirstOrDefault(p => p.Id == entity.Entity.Id);
			return Task.FromResult(new BaseDto<EditProductItemResultDto>(true, new List<string> { "Edit Product Is Success" }, mapper.Map<EditProductItemResultDto>(productResult)));
		}
	}
}
