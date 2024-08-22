namespace MooCleanCode.Application.Interfaces;

public interface IGameRepository
{
    public IEnumerable<(string name , int score)> GetToplistData();
    public void WriteToToplist(string name, int numberOfGuesses);
    public void SetToplistFilename(string filename);
}