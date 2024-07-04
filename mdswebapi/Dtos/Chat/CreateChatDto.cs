namespace mdswebapi.Dtos.Chat
{
    public class CreateChatDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
