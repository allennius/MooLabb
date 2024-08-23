using Microsoft.Extensions.DependencyInjection;
using MooCleanCode.Application;
using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Interfaces;
using MooCleanCode.Infrastructure.Repositories;
using MooCleanCode.Presentation;
using MooCleanCode.Presentation.Interfaces;
using MooCleanCode.Presentation.UI;

namespace MooCleanCode;

public static class DependencyInjection
{
    public static void AddGameServices(this IServiceCollection services)
    {
        services.AddSingleton<IUI, ConsoleUI>();
        services.AddSingleton<IGameRepository, GameRepository>();
        services.AddSingleton<IGame, Game>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IGameManager, GameManager>();
    }
}