namespace Chat_Service.Application.DTOs
{
    public record ChatDTO
    {
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public string MediaUrl { get; set; }
    }
}
