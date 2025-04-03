using StudentCenterAuthApi.src.Application.Interfaces;
using StudentCenterAuthApi.src.Application.Services;
using StudentCenterAuthApi.src.Domain.Interfaces;
using StudentCenterAuthApi.src.Infrastructure.Data.Authentication;
using StudentCenterAuthApi.src.Infrastructure.Data.Repositories;

namespace StudentCenterAuthApi.src.Infrastructure.Dependency_Injection;

public static class DependencyInjection 
{
    public static IServiceCollection AddInterfaces(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ITokenGenerator, TokenGenenator>();
        
        return services;
    }
}
