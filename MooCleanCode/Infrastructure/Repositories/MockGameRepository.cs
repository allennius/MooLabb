using MooCleanCode.Application.Interfaces;

namespace MooCleanCode.Infrastructure.Repositories;

public class MockGameRepository : IGameRepository
{

    private readonly List<(string name, int score)> toplist = new List<(string name, int score)>
    {
        ("Obi", 10),
        ("Anakin", 5),
        ("Yoda", 1)
    };

    public string toplistFilename = "";

    public IEnumerable<(string name, int score)> GetToplistData()
    {
        return toplist;
    }
    public void WriteToToplist(string name, int numberOfGuesses)
    {
        toplist.Add((name, numberOfGuesses));
    }
    public void SetToplistFilename(string filename)
    {
        toplistFilename = filename;
    }
}