using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Entities.Forum;

namespace WebRunApplication.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Indicator> Indicators { get; set; }

        public DbSet<TrainingTemplateType> TemplateTypes { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingTemplate> TrainingTemplates { get; set; }

        public DbSet<TrainingType> TrainingTypes { get; set; }

        public DbSet<MailingTopic> MailingTopics { get; set; }

        public DbSet<MailingMessage> Mailings { get; set; }

        public DbSet<MailingTopicSubscriber> MailingTopicSubscribers { get; set; }

        public DbSet<ForumReaction> ForumReactions { get; set; }
    }
}
