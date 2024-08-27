using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MooCleanCode;
using MooCleanCode.Presentation.Interfaces;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();
services.AddDataSource(configuration);
services.AddGameServices();

var serviceProvider = services.BuildServiceProvider();

var gameManager = serviceProvider.GetRequiredService<IGameManager>();
gameManager.Run();