using Microsoft.Extensions.DependencyInjection;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Repositories;
using WebRunApplication.Services.Implementations;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Models
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<ForumReaction>, ForumReactionRepository>();
            services.AddScoped<IBaseRepository<MailingMessage>, MailingRepository>();
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
            services.AddScoped<IHelpMessageService, HelpMessageMessageService>();
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IMailingService, MailingService>();
            services.AddScoped<IMailingTopicService, MailingTopicService>();
            services.AddScoped<IMailingTopicSubscriberService, MailingTopicSubscriberService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITrainingTemplateService, TrainingTemplateService>();
            services.AddScoped<IDataBaseGenerator, Infrastructure.DataBaseGeneratorService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IIndicatorService, IndicatorService>();
            services.AddScoped<ITrainingStatisticalService, TrainingStatisticalService>();
        }
    }
}
