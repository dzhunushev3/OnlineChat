using OnlineChat.Data;
using OnlineChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.Repositories
{
    public class ChatRepository : BaseRepository<ChatModel>
    {
        public ChatRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
