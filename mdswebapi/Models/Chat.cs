using System.ComponentModel.DataAnnotations.Schema;

namespace mdswebapi.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public string SenderId { get; set; } = string.Empty;

        public string ReceiverId { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty; 

        public virtual Customer? Sender { get; set; }  

        public virtual Customer? Receiver { get; set; }
    }
}
