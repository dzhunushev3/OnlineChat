using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Models;

namespace OnlineChat.Hub
{
    [Authorize]
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
        private static int Count = 0;
        private static List<string> OnlineUsers = new List<string>();
        public async Task SendMessage(string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "Guest", message);
            }
            catch(Exception ex)
            {
                string mess = ex.Message;
            }
        }
        public override async Task OnConnectedAsync()
        {
            Count++;
            OnlineUsers.Add(Context.User.Identity.Name);
            await Clients.All.SendAsync("OnlineUsers", OnlineUsers);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Count--;
            OnlineUsers.Remove(Context.User.Identity.Name);
            await Clients.All.SendAsync("OnlineUsers", OnlineUsers);
            await base.OnDisconnectedAsync(exception);
        }
        
    }

}                      
