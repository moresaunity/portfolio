
namespace Api.EndPoint.Models.Dtos.Product.Favourite
{
    public class GetFavouriteResultDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public GetFavouriteProductItemResultDto ProductItem { get; set; }
        public int ProductItemId { get; set; }
        public List<Links> Links { get; set; }
    }
}
