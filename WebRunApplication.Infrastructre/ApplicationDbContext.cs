using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Domain.Enums.Models;

namespace WebRunApplication.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //RefreshDataBase();
        }

        private void RefreshDataBase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            if (Users.Count() == 0)
            {
                Users.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateUsers());
            }

            if (Indicators.Count() == 0)
            {
                Indicators.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateIndicators());
            }

            if (Helps.Count() == 0)
            {
                Helps.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateHelps());
            }

            if (TemplateTypes.Count() == 0)
            {
                TemplateTypes.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateTemplateTypes());
            }

            if (Trainings.Count() == 0)
            {
                Trainings.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateTrainings());
            }

            if (TrainingTemplates.Count() == 0)
            {
                TrainingTemplates.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateTrainingTemplate());
            }

            if (TrainingTypes.Count() == 0)
            {
                TrainingTypes.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateTrainingTypes());
            }

            if (Mailings.Count() == 0)
            {
                Mailings.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateMailings());
            }

            if (MailingTopics.Count() == 0)
            {
                MailingTopics.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateMailingTopics());
            }

            if (MailingTopicSubscribers.Count() == 0)
            {
                MailingTopicSubscribers.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateMailingTopicSubscribers());
            }

            if (ForumMessages.Count() == 0)
            {
                ForumMessages.AddRange(Domain.Enums.Models.DataBaseGenerator.GenerateForumMessages());
            }
            
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Indicator> Indicators { get; set; }

        public DbSet<Help> Helps { get; set; }

        public DbSet<TemplateType> TemplateTypes { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingTemplate> TrainingTemplates { get; set; }

        public DbSet<TrainingType> TrainingTypes { get; set; }

        public DbSet<MailingTopic> MailingTopics { get; set; }

        public DbSet<Mailing> Mailings { get; set; }

        public DbSet<MailingTopicSubscriber> MailingTopicSubscribers { get; set; }

        public DbSet<ForumMessage> ForumMessages { get; set; }

        public DbSet<ForumReaction> ForumReactions { get; set; }
    }
}
