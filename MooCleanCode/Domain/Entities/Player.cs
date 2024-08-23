using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Entities;

public class Player(string name, int totalGuesses) : IPlayer
{
    private int totalGuesses = totalGuesses;
    public string Name { get; } = name;
    public int GamesPlayed { get; private set; } = 1;


    public void UpdatePlayerStats(int guesses)
    {
        totalGuesses += guesses;
        GamesPlayed++;
    }

    public double GetAverageGuessesPerGame()
    {
        return (double)totalGuesses / GamesPlayed;
    }


    public override bool Equals(object p)
    {
        return Name.Equals(((Player)p).Name);
    }


    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}