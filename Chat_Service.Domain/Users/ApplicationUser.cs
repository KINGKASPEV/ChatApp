using Chat_Service.Domain.ChatEntities;
using Microsoft.AspNetCore.Identity;


namespace Chat_Service.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
        public bool FirstTimeLogin { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public ICollection<Chat> SentChats { get; set; } = new HashSet<Chat>();
        public ICollection<Chat> ReceivedChats { get; set; } = new HashSet<Chat>();
        public ICollection<GroupUser> GroupMemberships { get; set; } = new HashSet<GroupUser>();
    }
}
