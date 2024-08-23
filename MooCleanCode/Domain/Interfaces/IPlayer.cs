namespace MooCleanCode.Domain.Interfaces;

public interface IPlayer
{
    public string Name { get; }
    public int GamesPlayed { get; }

    void UpdatePlayerStats(int guesses);
    double GetAverageGuessesPerGame();
    bool Equals(object p);
    int GetHashCode();
}