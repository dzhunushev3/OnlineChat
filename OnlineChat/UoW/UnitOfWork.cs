using OnlineChat.Data;
using OnlineChat.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.UoW
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public readonly MessageRepository MessageRepository;
        public readonly GroupRepository GroupRepository;
        public readonly ChatRepository ChatRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            MessageRepository = new MessageRepository(context);
            GroupRepository = new GroupRepository(context);
            ChatRepository = new ChatRepository(context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
