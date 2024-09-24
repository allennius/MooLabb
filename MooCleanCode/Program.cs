using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MooCleanCode;
using MooCleanCode.Presentation.Interfaces;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

// choose between mongo and file database by uncomment/comment out one of following lines
// services.AddMongoDBSource(configuration);
services.AddFileDatabase();


services.AddGameServices();

var serviceProvider = services.BuildServiceProvider();

var gameManager = serviceProvider.GetRequiredService<IGameManager>();
gameManager.Run();