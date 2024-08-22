using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Application;

public class GameService(Game game, IGameRepository repository)
{
    public string HandleGuess(string guess)
    {
        game.SetNumberOfGuesses(game.NumberOfGuesses + 1);
        return Game.CheckGuess(game.Goal, guess);
    }
    public string GetGoal()
    {
        return game.Goal;
    }
    public int GetNumberOfGuesses()
    {
        return game.NumberOfGuesses;
    }
    public void ResetGame()
    {
        game.SetGoal();
        game.SetNumberOfGuesses();
    }
    public void AddGameToToplist(string name)
    {
        if(game.NumberOfGuesses > 0)
            repository.WriteToToplist(name, game.NumberOfGuesses);
    }
    public IEnumerable<IPlayer> GetToplist()
    {
        var toplistData = repository.GetToplistData();

        var toplist = SummarizeToplistDataToPlayerTotals(toplistData);
        toplist.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));

        return toplist;
    }

    public void SetGameMode(GameType gameSelection)
    {
        var gameType = Enum.IsDefined(typeof(GameType), gameSelection)
            ? gameSelection
            : GameType.Default;
        
        repository.SetToplistFilename(TopListFilenames.GetFileName(gameType));
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
                summarizedToplist[playerIndex].Update(result.score);
        }

        return summarizedToplist;
    }
}