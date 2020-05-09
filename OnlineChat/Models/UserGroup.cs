using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.Models
{
    public class UserGroup:BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
    }
}
