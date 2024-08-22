using Microsoft.Extensions.DependencyInjection;
using MooCleanCode;
using MooCleanCode.Presentation.Interfaces;

var services = new ServiceCollection();
services.AddGameServices();

var serviceProvider = services.BuildServiceProvider();

var gameManager = serviceProvider.GetRequiredService<IGameManager>();
gameManager.Run();