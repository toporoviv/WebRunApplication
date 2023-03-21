using Microsoft.EntityFrameworkCore;
using WebRunApplication.DataEntity.Forum;
using WebRunApplication.Models;

namespace WebRunApplication.DataEntity
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
                Users.AddRange(DataBaseGenerator.GenerateUsers());
                SaveChanges();
            }

            if (Indicators.Count() == 0)
            {
                Indicators.AddRange(DataBaseGenerator.GenerateIndicators());
                SaveChanges();
            }

            if (Helps.Count() == 0)
            {
                Helps.AddRange(DataBaseGenerator.GenerateHelps());
                SaveChanges();
            }

            if (TemplateTypes.Count() == 0)
            {
                TemplateTypes.AddRange(DataBaseGenerator.GenerateTemplateTypes());
                SaveChanges();
            }

            if (Trainings.Count() == 0)
            {
                Trainings.AddRange(DataBaseGenerator.GenerateTrainings());
                SaveChanges();
            }

            if (TrainingTemplates.Count() == 0)
            {
                TrainingTemplates.AddRange(DataBaseGenerator.GenerateTrainingTemplate());
                SaveChanges();
            }

            if (TrainingTypes.Count() == 0)
            {
                TrainingTypes.AddRange(DataBaseGenerator.GenerateTrainingTypes());
                SaveChanges();
            }

            if (Mailings.Count() == 0)
            {
                Mailings.AddRange(DataBaseGenerator.GenerateMailings());
                SaveChanges();
            }

            if (MailingTopics.Count() == 0)
            {
                MailingTopics.AddRange(DataBaseGenerator.GenerateMailingTopics());
                SaveChanges();
            }

            if (MailingTopicSubscribers.Count() == 0)
            {
                MailingTopicSubscribers.AddRange(DataBaseGenerator.GenerateMailingTopicSubscribers());
                SaveChanges();
            }

            if (ForumMessages.Count() == 0)
            {
                ForumMessages.AddRange(DataBaseGenerator.GenerateForumMessages());
                SaveChanges();
            }
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
