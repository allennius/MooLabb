using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Application.Interfaces;

public interface IGameService
{
    public string HandleGuess(string guess);
    public string GetSecretCode();
    public int GetNumberOfGuesses();
    public void ResetGame();
    public void AddGameToToplist(string name);
    public IEnumerable<IPlayer> GetToplist();

    public void SetGameMode(GameType gameSelection);
}