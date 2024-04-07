using Application.Services.Products.ProductItem;
using FluentValidation;

namespace Aplication.Services.Products.ProductItem.Commands.Create
{
    public class CreateProductItemDto
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
    public class CreateProductItemDtoValidator : AbstractValidator<CreateProductItemDto>
    {
        public CreateProductItemDtoValidator()
        {
            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.Price).NotNull();
            RuleFor(p => p.Name).Length(2, 100);
            RuleFor(p => p.AvailableStock).InclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Price).InclusiveBetween(0, int.MaxValue);
        }
    }
}
