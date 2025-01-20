using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Services.Models;
using WebRunApplication.Services.Services.Interfaces;

namespace WebRunApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IHelpService _helpService;
        
        // todo: _mailSenderService не используется((
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
            var result = await _adminService.GetQuestionsAsync();
            
            if (result.StatusCode == Domain.Enums.StatusCode.OK) 
                return View(result.Data);

            ModelState.AddModelError("", result.Description);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Question(int id)
        {
            var result = await _helpService.GetAll();

            if (result.StatusCode == Domain.Enums.StatusCode.OK)
            { 
                return RedirectToAction("Answer", "Admin", result.Data.FirstOrDefault(x => x.Id == id));
            }

            ModelState.AddModelError("", result.Description);

            return RedirectToAction("Question", "Admin");
        }
        
        [HttpGet]
        public IActionResult Answer(HelpMessage model)
        {
            return model is null ? new EmptyResult() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Answer(int id, string answer)
        {
            var result = await _adminService.CreateAnswerAsync((uint)id, answer);

            if (result.StatusCode == Domain.Enums.StatusCode.OK) 
                return RedirectToAction("Question", "Admin");

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

            // todo: нужно обработать если будет null
            var topic = (await _mailingTopicService.GetAll()).Data.FirstOrDefault(topic => topic.Id == id).Title;

            var users = (await _userService.GetAllAsync()).Data.Where(user => indexes.Contains(user.Id)).ToList();

            await MailingTopics(users, topic, message);

            var result = await _mailingService.Create(new MailingMessage
            {
                Date = DateTime.Now,
                MailingTopicId = id,
                Message = message
            });

            if (result.StatusCode != Domain.Enums.StatusCode.OK) 
                ModelState.AddModelError("", result.Description);

            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        private async Task MailingTopics(List<User> users, string topic, string message)
        {
            foreach (var user in users)
            {
                if (string.IsNullOrWhiteSpace(user.Email)) // юзер мог не вводить свою почту, валидный случай (пока)
                    continue;
                
                // todo: вынести в конфиг почту
                var sender = new MailSender("runapp90@mail.ru", user.Email, "RunApp");
                
                // todo: надо бы раскидать этот процесс по таскам и использовать Task.WhenAll
                await sender.Send(topic, message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTopic(string newTopic)
        {
            // todo: надо убить этого кракена
            if ((await _mailingTopicService.GetAll()).Data.Where(x => x.Title == newTopic).Count() == 0)
            {
                var result = await _mailingTopicService.Create(new MailingTopic
                {
                    Title = newTopic 
                    
                });
                
                if (result.StatusCode != Domain.Enums.StatusCode.OK) 
                    ModelState.AddModelError("", result.Description);
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
            if (result.StatusCode != Domain.Enums.StatusCode.OK) 
                ModelState.AddModelError("", result.Description);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeTopic(int titles, string message)
        {
            var result = await _mailingTopicService.Update(new MailingTopic
            {
                Id = titles, 
                Title = message
            });

            if (result.StatusCode != Domain.Enums.StatusCode.OK) 
                ModelState.AddModelError("", result.Description);

            return RedirectToAction("PersonalAccount", "Main");
        }
    }
}
