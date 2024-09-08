using Microsoft.EntityFrameworkCore;
using Chat_Service.Domain.Users;

namespace Chat_Service.Application.Interfaces.Persisitence
{
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
    }
}
