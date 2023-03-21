using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using WebRunApplication.DataEntity;
using WebRunApplication.Enums;
using WebRunApplication.Models;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IHelpService _helpService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IMailingService _mailingService;
        private readonly IMailingTopicService _mailingTopicService;
        private readonly IMailingTopicSubscriberService _mailingTopicSubscriberService;

        public AdminController(
            IAdminService adminService, IHelpService helpService,
            IMailingTopicService mailingTopicService, IMailSenderService mailSenderService,
            IMailingTopicSubscriberService mailingTopicSubscriberService, IUserService userService,
            IMailingService mailingService)
        {
            _adminService = adminService;
            _helpService = helpService;
            _mailingTopicService = mailingTopicService;
            _mailSenderService = mailSenderService;
            _mailingTopicSubscriberService = mailingTopicSubscriberService;
            _userService = userService;
            _mailingService = mailingService;
        }

        [HttpGet]
        public async Task<IActionResult> Question()
        {
            var result = await _adminService.GetQuestions();
            
            if (result.StatusCode == Enums.StatusCode.OK) return View(result.Data);

            ModelState.AddModelError("", result.Description);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Question(int id)
        {
            var result = await _helpService.GetAll();

            if (result.StatusCode == Enums.StatusCode.OK)
            { 
                return RedirectToAction("Answer", "Admin", result.Data.FirstOrDefault(x => x.Id == id));
            }

            ModelState.AddModelError("", result.Description);

            return RedirectToAction("Question", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Answer(Help model)
        {
            return model is null ? new EmptyResult() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Answer(int id, string answer)
        {
            var result = await _adminService.CreateAnswer((uint)id, answer);

            if (result.StatusCode == Enums.StatusCode.OK) return RedirectToAction("Question", "Admin");

            ModelState.AddModelError("", result.Description);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Topic()
        {
            var list = (await _mailingTopicService.GetAll()).Data;
            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(list, "Id", "Title");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Topic(int id, string message)
        {
            var indexes = (await _mailingTopicSubscriberService
                .GetAll())
                .Data
                .Where(topic => topic.MailingTopicId == id)
                .Select(topic => topic.UserId)
                .ToList();

            var topic = (await _mailingTopicService.GetAll()).Data.FirstOrDefault(topic => topic.Id == id).Title;

            var users = (await _userService.GetAll()).Data.Where(user => indexes.Contains(user.Id)).ToList();

            await MailingTopics(users, topic, message);

            var result = await _mailingService.Create(new Mailing { Date = DateTime.Now, MailingTopicId = (uint)id, Message = message });

            if (result.StatusCode != Enums.StatusCode.OK) ModelState.AddModelError("", result.Description);

            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        private async Task MailingTopics(List<User> users, string topic, string message)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (!string.IsNullOrEmpty(users[i].Email))
                {
                    var sender = new MailSender("runapp90@mail.ru", users[i].Email, "RunApp");
                    await sender.Send(topic, message);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTopic(string newTopic)
        {
            if ((await _mailingTopicService.GetAll()).Data.Where(x => x.Title == newTopic).Count() == 0)
            {
                var result = await _mailingTopicService.Create(new MailingTopic { Title = newTopic });
                if (result.StatusCode != Enums.StatusCode.OK) ModelState.AddModelError("", result.Description);
            }

            return RedirectToAction("PersonalAccount", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTopic(int[] titles)
        {
            for (int i = 0; i < titles.Length; i++)
            {
                await RemoveTopic(titles[i]);
            }

            return RedirectToAction("PersonalAccount", "Main");
        }

        [NonAction]
        public async Task RemoveTopic(int id)
        {
            var result = await _mailingTopicService.Delete(id);
            if (result.StatusCode != Enums.StatusCode.OK) ModelState.AddModelError("", result.Description);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeTopic(int titles, string message)
        {
            var result = await _mailingTopicService.Update(new MailingTopic { Id = (uint)titles, Title = message });

            if (result.StatusCode != Enums.StatusCode.OK) ModelState.AddModelError("", result.Description);

            return RedirectToAction("PersonalAccount", "Main");
        }
    }
}
