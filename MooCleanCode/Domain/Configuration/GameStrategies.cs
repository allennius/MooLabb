using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;
using MooCleanCode.Domain.Strategies;

namespace MooCleanCode.Domain.Configuration;

public static class GameStrategies
{
    
    static private readonly Dictionary<GameType, IGameStrategy> gameStrategies = new ()
    {
        { GameType.Default, new MooGameStrategy() },
        { GameType.MooGame, new MooGameStrategy() },
        { GameType.MasterMindGame, new MasterMindGameStrategy() }
    };

    static private readonly Dictionary<GameType, string> gameRules = new ()
    {
        { GameType.Default, "4 unique numbers between 0-10." },
        { GameType.MooGame, "4 unique numbers between 0-10" },
        { GameType.MasterMindGame, "4 unique numbers between 0-5" }
    };

    public static IGameStrategy GetGameStrategy(GameType gameType)
    {
        return gameStrategies.GetValueOrDefault(gameType, gameStrategies[GameType.Default]);
    }

    public static string GetGameRules(GameType gameType)
    {
        return gameRules.GetValueOrDefault(gameType, gameRules[GameType.Default]);
    }
}