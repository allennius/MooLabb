using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Entities;

public class Player(string name, int totalGuess) : IPlayer
{
    public string Name { get; } = name;
    public int GamesPlayed { get; private set; } = 1;
    private int totalGuess = totalGuess;


    public void Update(int guesses)
    {
        totalGuess += guesses;
        GamesPlayed++;
    }

    public double Average()
    {
        return (double)totalGuess / GamesPlayed;
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