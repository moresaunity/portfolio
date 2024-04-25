namespace WebSite.EndPoint.Models.Dtos
{
    public class ChatMessageDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public ChatMessageDto(string Sender, string Message)
        {
            this.Sender = Sender;
            this.Message = Message;
        }
        public ChatMessageDto()
        {
            
        }
    }
}
