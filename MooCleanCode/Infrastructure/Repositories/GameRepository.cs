using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Enums;

namespace MooCleanCode.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly string lineSplit = "#&#";
    private string toplistFilename;

    public void SetToplistSource(GameType gameType)
    {
        string filename = TopListSourceNames.GetFileName(gameType);
        toplistFilename = filename + ".txt";
    }
    public void WriteToToplist(string name, int numberOfGuesses)
    {
        using var output = new StreamWriter(toplistFilename, true);
        output.WriteLine(name + lineSplit + numberOfGuesses);
    }

    public IEnumerable<(string name, int score)> GetToplistData()
    {
        var results = new List<(string name, int score)>();
        using var reader = new StreamReader(toplistFilename);
        while (reader.ReadLine() is { } line)
        {
            string[] nameAndScore = line.Split(lineSplit);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);
            results.Add((name, guesses));
        }
        return results;
    }
}