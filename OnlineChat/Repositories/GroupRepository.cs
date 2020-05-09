using Microsoft.EntityFrameworkCore;
using OnlineChat.Data;
using OnlineChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.Repositories
{
    public class GroupRepository : BaseRepository<Group>
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        internal async Task<List<Group>> GetAllPublicGroup()
        {
            return await DbSet.Where(g => g.IsPublic == true).ToListAsync();
        }

        internal async Task<Group> GetDefaultGroup()
        {
            return await DbSet.FirstOrDefaultAsync(g => g.Name == "PublicGroup");
        }

        public async Task<List<Group>> GetAllPublicGroupAsync()
        {
            return await DbSet
                //.Include(g=>g.UserList)
                .Where(g => g.IsPublic == true).ToListAsync();
        }
    }
}
