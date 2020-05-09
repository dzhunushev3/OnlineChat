using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineChat.Models;

namespace OnlineChat.ViewModels
{
    public class ChatGroupViewModel
    {
        public Group Group { get; set; }
        public List<Message> Messages { get; set; }
        public bool IsJoin { get; set; }
    }
}
