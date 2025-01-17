using Microsoft.AspNetCore.Cors.Infrastructure;
using WebRunApplication.Domain.Enums.DAL.Interfaces;
using WebRunApplication.Domain.Enums.DAL.Repositories;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Domain.Enums.Services.Implementations;
using WebRunApplication.Domain.Enums.Services.Interfaces;

namespace WebRunApplication.Domain.Enums.Models
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<ForumMessage>, ForumMessageRepository>();
            services.AddScoped<IBaseRepository<ForumReaction>, ForumReactionRepository>();
            services.AddScoped<IBaseRepository<Help>, HelpRepository>();
            services.AddScoped<IBaseRepository<Mailing>, MailingRepository>();
            services.AddScoped<IBaseRepository<MailingTopic>, MailingTopicRepository>();
            services.AddScoped<IBaseRepository<MailingTopicSubscriber>, MailingTopicSubscriberRepository>();
            services.AddScoped<IBaseRepository<Indicator>, IndicatorRepository>();
            services.AddScoped<IBaseRepository<Training>, TrainingRepository>();
            services.AddScoped<IBaseRepository<TrainingTemplate>, TrainingTemplateRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IPersonalAccountService, PersonalAccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IMailingService, MailingService>();
            services.AddScoped<IMailingTopicService, MailingTopicService>();
            services.AddScoped<IMailingTopicSubscriberService, MailingTopicSubscriberService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITrainingTemplateService, TrainingTemplateService>();
            services.AddScoped<IDataBaseGeneratorService, DataBaseGeneratorService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IIndicatorService, IndicatorService>();
            services.AddScoped<ITrainingStatisticalService, TrainingStatisticalService>();
        }
    }
}
