using Microsoft.Extensions.DependencyInjection;
using MooCleanCode.Application;
using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Infrastructure.Repositories;
using MooCleanCode.Presentation;
using MooCleanCode.Presentation.Interfaces;
using MooCleanCode.Presentation.UI;

namespace MooCleanCode;


public static class DependencyInjection
{
    public static IServiceCollection GameServices(this IServiceCollection services)
    {
        services.AddSingleton<IUI, ConsoleUI>();
        services.AddSingleton<IGameRepository, GameRepository>();
        services.AddSingleton<Game>();
        services.AddSingleton<GameService>();
        services.AddSingleton<IGameManager, GameManager>();

        return services;
    }
}