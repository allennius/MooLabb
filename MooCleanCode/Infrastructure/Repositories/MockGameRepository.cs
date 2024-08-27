using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Enums;

namespace MooCleanCode.Infrastructure.Repositories;

public class MockGameRepository : IGameRepository
{

    private readonly List<(string name, int score)> toplist =
    [
        ("Obi", 10),
        ("Anakin", 5),
        ("Yoda", 1)
    ];

    public string toplistFilename = "";

    public IEnumerable<(string name, int score)> GetToplistData()
    {
        return toplist;
    }
    public void WriteToToplist(string name, int numberOfGuesses)
    {
        toplist.Add((name, numberOfGuesses));
    }
    public void SetToplistSource(GameType gameType)
    {
        string filename = TopListSourceNames.GetFileName(gameType);
        toplistFilename = filename;
    }
}