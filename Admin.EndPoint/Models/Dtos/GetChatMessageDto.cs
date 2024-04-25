namespace Admin.EndPoint.Models.Dtos
{
    public class GetChatMessageDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
