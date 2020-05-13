
using OnlineChat.Data;
using OnlineChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineChat.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Message>> GetAllMessageIncludeAsync()
        {
            return await DbSet.Include(m => m.User)
                .Include(m=>m.Group)
                .ToListAsync();
        }

        public async Task<List<Message>> GetGroupMessagesByIdAsync(int id)
        {
            return await DbSet.Include(m=> m.User).Where(m => m.GroupId == id).ToListAsync();
        }


    }
}
