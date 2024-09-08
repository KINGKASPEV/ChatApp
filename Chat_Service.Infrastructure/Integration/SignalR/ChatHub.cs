using Chat_Service.Infrastructure.Integration.Client;
using Chat_Service.Application.Interfaces.Repositories;
using Chat_Service.Domain.ChatEntities;

namespace Chat_Service.Infrastructure.Integration.SignalR
{
    public class ChatHub : GenericHub<IChatClient>
    {
        private readonly IChatRepository _chatRepository;
        public ChatHub(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }


        public async Task SendMessageAsync(Chat chat)
        {
            await _chatRepository.CreateChatAsync(chat);
            await Clients.User(chat.ReceiverId).receiveChat(chat);
        }

        public async Task<List<string>> GetConnectedUser()
        {

            List<string?> data = OnlineUsers.Keys.ToList();
            return data;
        }

        public async Task UserIsTyping(Chat chat)
        {
            await Clients.User(chat.ReceiverId).sayWhoIsTyping(chat);
        }


    }
}
