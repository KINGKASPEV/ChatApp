using Chat_Service.Domain.ChatEntities;
using Chat_Service.Domain.Users;
using Chat_Service.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Chat_Service.Application.Interfaces.Repositories;

namespace Chat_Service.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly GenericRepository<Group> _context;
        private readonly AppDbContext _dbContext;

        public GroupRepository(GenericRepository<Group> context, AppDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            await _context.AddAsync(group);
            await _context.SaveAsync();
            return group;
        }

        public async Task<bool> DeleteGroupAsync(string groupId)
        {
            var group = await _context.GetByIdAsync(groupId);
            if (group is null)
                return false;

            await _context.RemoveAsync(group);
            return await _context.SaveAsync();
        }

        public async Task<Group> GetGroupByIdAsync(string groupId)
        {
            return await _dbContext.Groups
                .Include(g => g.GroupUsers)
                .ThenInclude(gu => gu.User)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<bool> AddUserToGroupAsync(string groupId, string userId)
        {
            var group = await _dbContext.Groups.FindAsync(groupId);
            var user = await _dbContext.Users.FindAsync(userId);

            if (group is null || user is null)
                return false;

            if (group.GroupUsers.Any(gu => gu.UserId == userId))
                return false; // User already in the group

            group.GroupUsers.Add(new GroupUser { GroupId = groupId, UserId = userId });
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromGroupAsync(string groupId, string userId)
        {
            var groupUser = await _dbContext.GroupUsers
                .FirstOrDefaultAsync(gu => gu.GroupId == groupId && gu.UserId == userId);

            if (groupUser is null)
                return false;

            _dbContext.GroupUsers.Remove(groupUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetGroupMembersAsync(string groupId)
        {
            return await _dbContext.GroupUsers
                .Where(gu => gu.GroupId == groupId)
                .Select(gu => gu.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetGroupChatsAsync(string groupId)
        {
            return await _dbContext.Chats
                .Where(c => c.GroupId == groupId)
                .OrderBy(c => c.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsByNameAsync(string name)
        {
            return await _dbContext.Groups
                .Where(g => g.Name.Contains(name))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsByUserAsync(string userId)
        {
            return await _dbContext.GroupUsers
                .Where(gu => gu.UserId == userId)
                .Select(gu => gu.Group)
                .ToListAsync();
        }
    }
}
