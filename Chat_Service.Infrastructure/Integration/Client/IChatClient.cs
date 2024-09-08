using Chat_Service.Domain.ChatEntities;

namespace Chat_Service.Infrastructure.Integration.Client
{
    public interface IChatClient
    {
        Task receiveChat(Chat chat);
        Task sayWhoIsTyping(Chat chat);
    }
}
