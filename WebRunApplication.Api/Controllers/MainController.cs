using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebRunApplication.Services.Models;
using WebRunApplication.Services.Services.Interfaces;

namespace WebRunApplication.Controllers
{
    [Authorize]
    // todo: странное название(
    public class MainController : Controller
    {
        private readonly IPersonalAccountService _personalAccountService;
        private readonly IAdminService _adminService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IUserService _userService;
        private readonly IMailingTopicService _mailingTopicService;
        
        public MainController(
            IPersonalAccountService personalAccountService,
            IUserService userService, IAdminService adminService,
            IMailSenderService mailSenderService, IMailingTopicService mailingTopicService)
        {
            _personalAccountService = personalAccountService;
            _userService = userService;
            _adminService = adminService;
            _mailSenderService = mailSenderService;
            _mailingTopicService = mailingTopicService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PersonalAccount()
        {
            var users = await _userService.GetAllAsync();
            
            // todo: user может быть null
            var user = users.Data.FirstOrDefault(x => x.Login == User.Identity!.Name);
            var result = await _personalAccountService.GetTrainingsAsync(user.Login);

            if (result.StatusCode != Domain.Enums.StatusCode.OK)
            {
                ModelState.AddModelError("", result.Description);
                return RedirectToAction("Index", "Home");
            }

            // todo: поменять название
            var _user = new UserViewModel
            {
                Trainings = result.Data,
                User = user
            };

            var dictResult = (await _adminService.GetTopicsInformationAsync()).Data;
            // todo: страшно
            ViewBag.TitlesDictionary = new Dictionary<string, (int, int, int)>(dictResult);

            var mailings = (await _mailingTopicService.GetAll()).Data.ToList();
            ViewBag.Titles = new SelectList(mailings, "Id", "Title");

            return View(_user);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail()
        {
            string? message = Request.Form["message"];

            var user = (await _userService.GetAllAsync()).Data.FirstOrDefault(u => u.Login == User.Identity.Name);
            
            // todo: вынести в конфиг
            var result = await _mailSenderService.SendMessage(
                (uint)user.Id, 
                "ya.toporow@gmail.com",
                message,
                "Вопрос"
            );

            if (result.StatusCode != Domain.Enums.StatusCode.OK) ModelState.AddModelError("", result.Description);

            return RedirectToAction("PersonalAccount", "Main");
        }

        [HttpGet]
        public async Task<IActionResult> Subscribe()
        {
            var result = await _personalAccountService.GetMailingTopicsAsync();

            if (result.StatusCode == Domain.Enums.StatusCode.OK)
            {
                ViewBag.Titles = new MultiSelectList(result.Data, "Id", "Title");
            }
            else
            {
                ModelState.AddModelError("", result.Description);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(int[] titles)
        {
            var result = await _personalAccountService.CreateSubscribeAsync(User.Identity.Name, titles);

            if (!result.Data) 
                ModelState.AddModelError("", result.Description);

            return RedirectToAction("PersonalAccount", "Main");
        }
    }
}
