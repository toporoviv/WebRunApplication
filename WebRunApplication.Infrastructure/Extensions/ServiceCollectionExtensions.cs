using Microsoft.Extensions.DependencyInjection;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Repositories;

namespace WebRunApplication.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHelpMessageRepository, HelpMessageRepository>();
        services.AddScoped<IForumMessageRepository, ForumMessageRepository>();
    }
}