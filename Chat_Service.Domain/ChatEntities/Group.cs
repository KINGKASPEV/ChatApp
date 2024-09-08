using Chat_Service.Domain.Common;
using Chat_Service.Domain.Users;

namespace Chat_Service.Domain.ChatEntities
{
    public class Group : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<Chat> Chats { get; set; }
    }
}
