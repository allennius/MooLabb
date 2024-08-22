using MooCleanCode.Application.Interfaces;

namespace MooCleanCode.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private string toplistFilename;
    private readonly string lineSplit = "#&#";

    public void SetToplistFilename(string filename)
    {
        toplistFilename = filename;
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