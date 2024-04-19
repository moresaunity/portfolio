using Application.Interfaces.Contexts;
using Domain.Dtos;
using MediatR;

namespace Aplication.Services.Products.ProductType.Commands.Send
{
    public class SendCommentHandler : IRequestHandler<SendProductTypeCommand, BaseDto<SendProductTypeResponseDto>>
    {
        private readonly IDataBaseContext context;

        public SendCommentHandler(IDataBaseContext context)
        {
            this.context = context;
        }
        public Task<BaseDto<SendProductTypeResponseDto>> Handle(SendProductTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Products.ProductType ProductType = new Domain.Products.ProductType
            {
                Type = request.ProductTypeDto.Type,
                ParentProductTypeId = request.ProductTypeDto.ParentProductTypeId
            };
            var entity = context.ProductTypes.Add(ProductType);
            context.SaveChanges();

            return Task.FromResult(new BaseDto<SendProductTypeResponseDto>(true, new List<string> { "Add a New ProductType Is Success" }, new SendProductTypeResponseDto
            {
                Id = entity.Entity.Id,
                Type = ProductType.Type,
                ParentProductTypeId = ProductType.ParentProductTypeId
            }));
        }
    }

}
