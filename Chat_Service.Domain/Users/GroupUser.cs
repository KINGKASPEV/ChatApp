using Chat_Service.Domain.ChatEntities;

namespace Chat_Service.Domain.Users
{
    public class GroupUser
    {
        public string GroupId { get; set; }
        public Group Group { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
