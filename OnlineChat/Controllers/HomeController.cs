using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OnlineChat.Hub;
using OnlineChat.Models;
using OnlineChat.Services;

namespace OnlineChat.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
       // private readonly IHubContext<Chat> _hub;
        private readonly UserManager<User> _userManager;
        private readonly MessageService _messageService;

        public HomeController(UserManager<User> userManager, MessageService messageService)
        {
            _userManager = userManager;
            _messageService = messageService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //await _messageService.Index();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task SendMessage(string message,int idGroup)
        {
            try{
                var user = await _userManager.GetUserAsync(User);
                await _messageService.SendMessage(user, message, idGroup);

               
            }
            catch(Exception ex)
            {
                var mess = ex.Message;
            }
        }

        public async Task<JsonResult> GetAllMessage()
        {
            var message = await _messageService.GetAllMessage();
            return Json(message);
        }

        public async Task<JsonResult> GetGroups()
        {
            var groups = await _messageService.GetAllPublicGroups();
            return Json(groups);
        }
        public async Task ShowGroups()
        {
            await _messageService.ShowPublicGroup();
        }
        public async Task<JsonResult> CreateGroup(string nameGroup,bool status)
        {
            await _messageService.CreateGroupAsync(nameGroup, status);
            return Json(new {Message = "Ok"});
        }
        [Authorize]
        public async Task<IActionResult> SelectGroup(int id)
        { 
            var user = await _userManager.GetUserAsync(User);
            var chatGroupViewModel = await _messageService.GetGroupMessage(id,user);
            return PartialView("_ChatPartialView", chatGroupViewModel);
        }
    }
}
