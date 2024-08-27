using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Application;

public class GameService(IGame game, IGameRepository repository) : IGameService
{
    public string HandleGuess(string guess)
    {
        game.SetNumberOfGuesses(game.NumberOfGuesses + 1);
        return game.EvaluateGuessForBullsAndCows(game.SecretCode, guess);
    }
    public string GetSecretCode()
    {
        return game.SecretCode;
    }
    public int GetNumberOfGuesses()
    {
        return game.NumberOfGuesses;
    }
    public void ResetGame()
    {
        game.SetSecretCode();
        game.SetNumberOfGuesses();
    }
    public void AddGameToToplist(string name)
    {
        if (game.NumberOfGuesses > 0)
            repository.WriteToToplist(name, game.NumberOfGuesses);
    }
    public IEnumerable<IPlayer> GetToplist()
    {
        var toplistData = repository.GetToplistData();

        var toplist = SummarizeToplistDataToPlayerTotals(toplistData);
        toplist.Sort((p1, p2) => p1.GetAverageGuessesPerGame().CompareTo(p2.GetAverageGuessesPerGame()));

        return toplist;
    }

    public void SetGameMode(GameType gameSelection)
    {
        var gameType = Enum.IsDefined(typeof(GameType), gameSelection)
            ? gameSelection
            : GameType.Default;

        repository.SetToplistSource(gameSelection);
        game.SetGameStrategy(GameStrategies.GetGameStrategy(gameType));
    }

    static private List<IPlayer> SummarizeToplistDataToPlayerTotals(IEnumerable<(string name, int score)> toplistData)
    {
        var summarizedToplist = new List<IPlayer>();
        foreach (var result in toplistData)
        {
            var player = new Player(result.name, result.score);
            int playerIndex = summarizedToplist.IndexOf(player);
            if (playerIndex < 0)
                summarizedToplist.Add(player);
            else
                summarizedToplist[playerIndex].UpdatePlayerStats(result.score);
        }

        return summarizedToplist;
    }
}