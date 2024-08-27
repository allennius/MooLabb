using MongoDB.Driver;
using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Infrastructure.Dtos;

namespace MooCleanCode.Infrastructure.Repositories;

public class MongoDBGameRepository : IGameRepository
{
    private readonly IMongoDatabase database;
    private IMongoCollection<PlayerDto> collection;

    public MongoDBGameRepository(IMongoDatabase database, GameType gameType)
    {
        this.database = database ?? throw new ArgumentNullException(nameof(database));
        SetToplistSource(gameType);
    }
    public IEnumerable<(string name, int score)> GetToplistData()
    {
        var playerDtos = collection.Find(p => true).ToList();
        var results = new List<(string name, int score)>();

        foreach (var player in playerDtos)
        {
            results.Add((player.Name, player.NumberOfGuesses));
        }

        return results;
    }
    public void WriteToToplist(string name, int numberOfGuesses)
    {
        var playerDto = new PlayerDto
        {
            Name = name,
            NumberOfGuesses = numberOfGuesses
        };
        collection.InsertOne(playerDto);
    }
    public void SetToplistSource(GameType gameType)
    {
        string collectionName = TopListSourceNames.GetCollectionName(gameType);
        collection = database.GetCollection<PlayerDto>(collectionName);
    }
}