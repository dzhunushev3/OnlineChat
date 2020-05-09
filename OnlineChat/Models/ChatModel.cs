using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.Models
{
    public class ChatModel :BaseEntity
    {
        public string UserId1 { get; set; }
        public User User1 { get; set; }
        public string UserId2 { get; set; }
        public User User2 { get; set; }

    }
}
