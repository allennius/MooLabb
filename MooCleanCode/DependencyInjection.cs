using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MooCleanCode.Application;
using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;
using MooCleanCode.Infrastructure.Repositories;
using MooCleanCode.Presentation;
using MooCleanCode.Presentation.Interfaces;
using MooCleanCode.Presentation.UI;

namespace MooCleanCode;

public static class DependencyInjection
{

    public static void AddDataSource(this IServiceCollection services, IConfiguration configuration)
    {
        // mongoDatabase
        services.AddSingleton<IGameRepository, MongoDBGameRepository>(sp =>
        {
            string? connectionString = configuration.GetConnectionString("MongoDB");
            var mongoUrl = new MongoUrl(connectionString);
            var database = new MongoClient(connectionString)
                .GetDatabase(mongoUrl.DatabaseName);
            return new MongoDBGameRepository(database, GameType.Default);
        });

        // fileDatabase
        // services.AddSingleton<IGameRepository, GameRepository>();
    }
    public static void AddGameServices(this IServiceCollection services)
    {
        services.AddSingleton<IUI, ConsoleUI>();
        services.AddSingleton<IGame, Game>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IGameManager, GameManager>();
    }
}