using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.Models
{
    public class Group: BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPublic { get; set; }
        [NotMapped]
        public List<User> UserList { get; set; }

        public Group()
        {
            UserList = new List<User>();
        }
    }
}
