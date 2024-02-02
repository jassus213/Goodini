using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Dal;
using User.Dal.Interfaces;
using User.Dal.PostgreSql;

namespace User.StartUp;

public static class ServiceCollectionExtension
{
    public static void IncludeUserFeature(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<UserContext>(builder => builder.UseNpgsql(connectionString),
            ServiceLifetime.Singleton);

        serviceCollection.AddSingleton<IUserRepository, UserRepository>();
    }
}