namespace Api.EndPoint.Models.Dtos
{
    public class AccountResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }
        public ReturnLoginViewModel Model { get; set; }
    }
}
