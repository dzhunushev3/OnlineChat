using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineChat.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        public List<Group> GroupList { get; set; }  
        
    }
}
