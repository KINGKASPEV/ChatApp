using Chat_Service.Application.Interfaces.Repositories;
using Chat_Service.Domain.ChatEntities;
using Chat_Service.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat_Service.Persistence.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly GenericRepository<Chat> _context;
        private readonly AppDbContext _dbContext;

        public ChatRepository(GenericRepository<Chat> context, AppDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await _context.AddAsync(chat);
            await _context.SaveAsync();
        }

        public async Task<bool> DeleteChatAsync(string chatId)
        {
            var chat = await _context.GetByIdAsync(chatId);
            if (chat is null)
                return false;

            await _context.RemoveAsync(chat);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeleteChatHistoryAsync(string userId, string receiverId)
        {
            var chats = _context.FindByCondition(c => (c.SenderId == userId || c.ReceiverId == userId)
                                                               && (c.ReceiverId == receiverId || c.SenderId == receiverId),
                                                           trackChanges: false);
            _context.RemoveRangeAsync(chats);
            return await _context.SaveAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllChatHistoryAsync(string userId)
        {
            return await _dbContext.Chats
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .OrderByDescending(c => c.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetRecentChatsAsync(string userId)
        {
            return await _dbContext.Chats
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .GroupBy(c => new { c.SenderId, c.ReceiverId })
                .Select(g => g.OrderByDescending(c => c.Timestamp).FirstOrDefault())
                .OrderByDescending(c => c.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetChatWithUserAsync(string userId, string receiverId)
        {
            return await _dbContext.Chats
                .Where(c => (c.SenderId == userId && c.ReceiverId == receiverId) ||
                            (c.SenderId == receiverId && c.ReceiverId == userId))
                .OrderBy(c => c.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetChatsInGroupAsync(string groupId)
        {
            return await _dbContext.Chats
                .Where(c => c.GroupId == groupId)
                .OrderBy(c => c.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetRecentChatsInGroupAsync(string groupId)
        {
            return await _dbContext.Chats
                .Where(c => c.GroupId == groupId)
                .GroupBy(c => c.SenderId)
                .Select(g => g.OrderByDescending(c => c.Timestamp).FirstOrDefault())
                .OrderByDescending(c => c.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
