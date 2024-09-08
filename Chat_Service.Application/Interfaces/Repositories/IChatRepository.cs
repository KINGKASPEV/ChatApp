using Chat_Service.Domain.ChatEntities;

namespace Chat_Service.Application.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task CreateChatAsync(Chat createChatDTO);
        Task<bool> DeleteChatAsync(string chatId);
        Task<bool> DeleteChatHistoryAsync(string userId, string receiverId);
        Task<IEnumerable<Chat>> GetAllChatHistoryAsync(string userId);
        Task<IEnumerable<Chat>> GetRecentChatsAsync(string userId);
        Task<IEnumerable<Chat>> GetChatWithUserAsync(string userId, string receiverId);
        Task<IEnumerable<Chat>> GetChatsInGroupAsync(string groupId);
        Task<IEnumerable<Chat>> GetRecentChatsInGroupAsync(string groupId);
    }
}
