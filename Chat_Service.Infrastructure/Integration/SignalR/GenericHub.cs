using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Chat_Service.Infrastructure.Integration.SignalR
{
    public class GenericHub<TClient> : Hub<TClient> where TClient : class
    {

        public static ConcurrentDictionary<string, List<string>> OnlineUsers =
            new ConcurrentDictionary<string, List<string>>();

        public GenericHub() { }

        public override async Task OnConnectedAsync()
        {
            string? userid = Context.User?.Identity?.Name;
            if (userid == null || userid.Equals(string.Empty))
            {
                Trace.TraceInformation("User not logged in can't connect signalr service");
                return;
            }


            Trace.TraceInformation(userid + "connected");
            // save connection
            List<string>? existUserConnectionIds;
            OnlineUsers.TryGetValue(userid, out existUserConnectionIds);
            existUserConnectionIds ??= new List<string>();
            existUserConnectionIds.Add(Context.ConnectionId);
            OnlineUsers.TryAdd($"{userid}-{Context.UserIdentifier}", existUserConnectionIds);
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string? userid = Context.User?.Identity?.Name;
            if (userid == null || userid.Equals(string.Empty))
            {
                Trace.TraceInformation("User not logged in can't connect signalr service");
                return;
            }
            if (OnlineUsers.ContainsKey($"{userid}-{Context.UserIdentifier}"))
            {
                OnlineUsers.TryRemove($"{userid}-{Context.UserIdentifier}", out List<string>? existUserConnectionIds);
                existUserConnectionIds.Remove(Context.ConnectionId);
                OnlineUsers.TryAdd($"{userid}-{Context.UserIdentifier}", existUserConnectionIds);

            }
            await base.OnDisconnectedAsync(exception);
        }

    }
}
