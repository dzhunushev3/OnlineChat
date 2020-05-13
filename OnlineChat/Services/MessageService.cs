using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OnlineChat.Hub;
using OnlineChat.Models;
using OnlineChat.UoW;
using OnlineChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OnlineChat.Services
{
    public class MessageService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<Chat> _hub;
        public MessageService(UnitOfWork unitOfWork,UserManager<User> userManager, IHubContext<Chat> hub)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _hub = hub;

        }
        public async Task SendMessage(User user,string message, int? idGroup)
        {
            try
            {
                Message message1 = new Message()
                {
                    Messag = message,
                    UserId = user.Id,
                    GroupId = idGroup,
                    DateTime = DateTime.Now
                };
                await _unitOfWork.MessageRepository.CreateAsync(message1);
                await _unitOfWork.CompleteAsync();
                await _hub.Clients.All.SendAsync("ReceiveMessage",idGroup, user.UserName , message,DateTime.Now);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Message>> GetAllMessage()
        {
            //await ShowPublicGroup();
            return await _unitOfWork.MessageRepository.GetAllMessageIncludeAsync();
        }

        public async Task CreateGroupAsync(string nameGroup, bool status)
        {
            Group group = new Group()
            {
                Name = nameGroup,
                DateCreated = DateTime.Now,
                IsPublic = status,
                UserList = new List<User>()
            };
            await _unitOfWork.GroupRepository.CreateAsync(group);
            await _unitOfWork.CompleteAsync();
            await _hub.Clients.All.SendAsync("UpdateGroup");
        }

        public async Task ShowPublicGroup()
        {
            var groups = await _unitOfWork.GroupRepository.GetAllPublicGroupAsync();
            await _hub.Clients.All.SendAsync("ListGroup", groups);
        }

        public async Task<List<Group>> GetAllPublicGroups()
        {
            return await _unitOfWork.GroupRepository.GetAllPublicGroup();
        }

        public async Task<ChatGroupViewModel> GetGroupMessage(int id,User user)
        {
            var messages = await _unitOfWork.MessageRepository.GetGroupMessagesByIdAsync(id);
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(id);
            bool isJoin = group.UserList.Contains(user);
            ChatGroupViewModel chatGroupViewModel = new ChatGroupViewModel()
            {
                Messages = messages ,
                Group = group,
                IsJoin = isJoin
            };
            return chatGroupViewModel;
        }

        public async Task<List<Message>> SearchMessages(string searchParam, int idGroup)
        {
            var messages = await _unitOfWork.MessageRepository.GetGroupMessagesByIdAsync(idGroup);
            if (searchParam.IndexOf("@") == 0)
            {
                messages = messages.Where(x => (x.Messag?.ToUpper() ?? String.Empty).Contains(searchParam.ToUpper())
                    || (x.User.UserName?.ToUpper()??String.Empty).Contains(searchParam.ToUpper()))
                    .ToList();
            }
            else
            {
                messages = messages.Where(x => (x.Messag?.ToUpper() ?? String.Empty).Contains(searchParam.ToUpper()))
                    .ToList();
            }

            return messages;

        }
    }
}
