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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Indicator> Indicators { get; set; }

        public DbSet<HelpMessage> Helps { get; set; }

        public DbSet<TrainingTemplateType> TemplateTypes { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingTemplate> TrainingTemplates { get; set; }

        public DbSet<TrainingType> TrainingTypes { get; set; }

        public DbSet<MailingTopic> MailingTopics { get; set; }

        public DbSet<MailingMessage> Mailings { get; set; }

        public DbSet<MailingTopicSubscriber> MailingTopicSubscribers { get; set; }

        public DbSet<ForumMessage> ForumMessages { get; set; }

        public DbSet<ForumReaction> ForumReactions { get; set; }
    }
}
