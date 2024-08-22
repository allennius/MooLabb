namespace MooCleanCode.Domain.Interfaces;

public interface IPlayer 
{
    public string Name { get; }
    public int GamesPlayed { get; }
    
    void Update(int guesses);
    double Average();
    bool Equals(object p);
    int GetHashCode();
}