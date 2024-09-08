using Chat_Service.Domain.Common;
using Chat_Service.Domain.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Service.Domain.ChatEntities
{
    public class Chat : Entity
    {
        public string Message { get; set; }
        public string SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }

        public string GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; }
        public string MediaUrl { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
