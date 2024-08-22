using MooCleanCode.Application.Interfaces;

namespace MooCleanCode.Infrastructure.Repositories;

public class MockGameRepository : IGameRepository
{

    public string toplistFilename = "";
    
    private List<(string name, int score)> toplist;

    public MockGameRepository()
    {
        toplist = new ()
        {
            ("Obi", 10),
            ("Anakin", 5),
            ("Yoda", 1)
        };
    }
    
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