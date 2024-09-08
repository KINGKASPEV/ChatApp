using Chat_Service.Domain.ChatEntities;
using Chat_Service.Domain.Users;

namespace Chat_Service.Application.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<Group> CreateGroupAsync(Group group);
        Task<bool> DeleteGroupAsync(string groupId);
        Task<Group> GetGroupByIdAsync(string groupId);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<bool> AddUserToGroupAsync(string groupId, string userId);
        Task<bool> RemoveUserFromGroupAsync(string groupId, string userId);
        Task<IEnumerable<ApplicationUser>> GetGroupMembersAsync(string groupId);
        Task<IEnumerable<Chat>> GetGroupChatsAsync(string groupId);
        Task<IEnumerable<Group>> GetGroupsByNameAsync(string name);
        Task<IEnumerable<Group>> GetGroupsByUserAsync(string userId);
    }
}
