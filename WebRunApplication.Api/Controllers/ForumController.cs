using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Domain.Enums.Forum;
using WebRunApplication.Infrastructure;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Controllers
{
    public class ForumController
    (
        ApplicationDbContext context,
        IUserRepository userRepository
    ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var forumMessages = await GetForumMessages();
            
            return View(forumMessages);
        }

        [NonAction]
        private async Task<List<MessageViewModel>> GetForumMessages()
        {
            // todo: тут надо сделать проверку на null (не факт, что одну...)

            var users = (await userRepository.GetUsersAsync()).ToList();
            
            var messageViewModels = context.ForumMessages.Select(forumMessage => new MessageViewModel
            {
                Id = forumMessage.Id,
                ParentId = forumMessage.ParentId,
                Fullname = users.FirstOrDefault(z => z.Id == forumMessage.UserId).Fullname,
                Message = forumMessage.Message,
                Date = forumMessage.Date,
                LikedUsers = users
                    .Join(context.ForumReactions
                    .Where(
                        y => y.Reaction == ReactionType.Like && y.MessageId == forumMessage.Id),
                        u => u.Id,
                        fr => fr.UserId, 
                        (u, fr) => u)
                    .ToList(),
                DislikedUsers = users
                    .Join(context.ForumReactions
                    .Where(
                        y => y.Reaction != ReactionType.Like && y.MessageId == forumMessage.Id),
                        u => u.Id,
                        fr => fr.UserId,
                        (u, fr) => u)
                    .ToList(),
                NestingLevel = 0
            }).OrderByDescending(x => x.ParentId == null).ThenByDescending(x => x.Date).ToList();

            var result = new List<MessageViewModel>();

            foreach (var messageViewModel in messageViewModels)
            {
                if (messageViewModel.ParentId is null) 
                    result = GetMessages(result, messageViewModels, messageViewModel, 0);
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
            
            var orderedMessagesViewModels = messageViewModels
                .Where(m => m.ParentId == currentMessage.Id)
                .OrderByDescending(x => x.Date)
                .ToList();
            
            foreach (var messageViewModel in orderedMessagesViewModels)
            {
                result = GetMessages(result, messageViewModels, messageViewModel, currentLevel + 1);
            }

            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(string message)
        {
            // todo: User.Identity может ли быть null в данном случае?
            // todo: user может быть null
            var user = (await userRepository.GetUsersAsync())
                .FirstOrDefault(x => x.Login == User.Identity!.Name);
            
            context.ForumMessages.Add(new ForumMessage
            {
                Date = DateTime.Now,
                UserId = user.Id,
                Message = message,
                ParentId = null
            });
            
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> MessageReaction(int messageId, ReactionType reaction)
        {
            // todo: user может быть null
            var user = (await userRepository.GetUsersAsync())
                .FirstOrDefault(x => x.Login == User.Identity!.Name);

            var currentReaction = await context.ForumReactions
                .FirstOrDefaultAsync(x => x.MessageId == messageId);

            if (currentReaction is not null)
            {
                if (currentReaction.Reaction != reaction)
                {
                    context.ForumReactions.Remove(currentReaction);
                    await context.ForumReactions.AddAsync(new ForumReaction
                    {
                        MessageId = messageId,
                        UserId = user.Id,
                        Reaction = reaction
                    });
                }
                else
                {
                    context.ForumReactions.Remove(currentReaction);
                }
            }
            else
            {
                await context.ForumReactions.AddAsync(new ForumReaction
                {
                    MessageId = messageId,
                    UserId = user.Id,
                    Reaction = reaction
                });
            }

            await context.SaveChangesAsync();

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
            var user = (await userRepository.GetUsersAsync())
                .FirstOrDefault(x => x.Login == User.Identity!.Name);

            await context.ForumMessages.AddAsync(new ForumMessage 
            {
                Date = DateTime.Now,
                Message = message,
                ParentId = messageId,
                UserId = user.Id 
            });

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Forum");
        }
    }
}