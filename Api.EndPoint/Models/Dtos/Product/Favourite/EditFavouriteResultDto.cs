namespace Api.EndPoint.Models.Dtos.Product.Favourite
{
    public class EditFavouriteResultDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public List<Links> Links { get; set; }
    }
}
