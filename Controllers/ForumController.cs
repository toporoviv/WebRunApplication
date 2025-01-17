using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Domain.Enums.Models;

namespace WebRunApplication.Domain.Enums.Controllers
{
    public class ForumController : Controller
    {
        ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var forumMessages = await GetForumMessages();
            //ViewBag.ForumMessages = forumMessages;
            return View(forumMessages);
        }

        [NonAction]
        private async Task<List<MessageViewModel>> GetForumMessages()
        {
            // todo: тут надо сделать проверку на null (не факт, что одну...)
            var list = _context.ForumMessages.Select(x => new MessageViewModel
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Fullname = _context.Users.FirstOrDefault(z => z.Id == x.UserId).Fullname,
                Message = x.Message,
                Date = x.Date,
                LikedUsers = _context.Users
                    .Join(_context.ForumReactions
                    .Where(y => y.IsLike && y.MessageId == x.Id), u => u.Id, fr => fr.UserId, (u, fr) => u)
                    .ToList(),
                DislikedUsers = _context.Users
                    .Join(_context.ForumReactions
                    .Where(y => !y.IsLike && y.MessageId == x.Id), u => u.Id, fr => fr.UserId, (u, fr) => u)
                    .ToList(),
                NestingLevel = 0
            }).OrderByDescending(x => x.ParentId == null).ThenByDescending(x => x.Date).ToList();

            var result = new List<MessageViewModel>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ParentId is null) result = GetMessages(result, list, list[i], 0);
                else break;
            }

            return result;
        }

        [NonAction]
        private List<MessageViewModel> GetMessages
        (
            List<MessageViewModel> result,
            List<MessageViewModel> messageViewModels,
            MessageViewModel currentMessage,
            uint currentLevel
        )
        {
            currentMessage.NestingLevel = currentLevel;
            result.Add(currentMessage);
            var list = messageViewModels
                .Where(m => m.ParentId == currentMessage.Id)
                .OrderByDescending(x => x.Date)
                .ToList();
            
            for (var i = 0; i < list.Count; i++)
            {
                result = GetMessages(result, messageViewModels, list[i], currentLevel + 1);
            }

            return result;
        }

        [HttpPost]
        [Authorize]
        //[NonAction]
        public async Task<IActionResult> Send(string message)
        {
            // todo: User.Identity может ли быть null в данном случае?
            // todo: user может быть null
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == User.Identity.Name);

            _context.ForumMessages.Add(new ForumMessage { Date = DateTime.Now, UserId = user.Id, Message = message, ParentId = null });
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> MessageReaction(int messageId, bool isLike)
        {
            // todo: user может быть null
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == User.Identity!.Name);

            var reaction = await _context.ForumReactions
                .FirstOrDefaultAsync(x => x.MessageId == messageId);

            if (reaction is not null)
            {
                if (reaction.IsLike != isLike)
                {
                    _context.ForumReactions.Remove(reaction);
                    await _context.ForumReactions.AddAsync(new ForumReaction
                    {
                        MessageId = messageId,
                        UserId = user.Id,
                        IsLike = isLike
                    });
                }
                else
                {
                    _context.ForumReactions.Remove(reaction);
                }
            }
            else
            {
                await _context.ForumReactions.AddAsync(new ForumReaction()
                {
                    MessageId = messageId,
                    UserId = user.Id,
                    IsLike = isLike
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SendComment(int messageId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                // todo: тут нужно выкинуть exception 
                message = " ";
            }

            // todo: user может быть null
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == User.Identity!.Name);

            await _context.ForumMessages.AddAsync(new ForumMessage 
            {
                Date = DateTime.Now,
                Message = message,
                ParentId = messageId,
                UserId = user.Id 
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Forum");
        }
    }
}