using CSharpApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpApp.Application.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();

        services.AddScoped<IPostService, PostService>();

        return services;
    }
}