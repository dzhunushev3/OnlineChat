using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateTime = System.DateTime;

namespace OnlineChat.Models
{
    public class Message : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string Messag { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }
        public DateTime DateTime { get; set; }

    }
}
