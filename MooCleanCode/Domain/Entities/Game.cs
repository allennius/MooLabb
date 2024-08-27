using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Entities;

public class Game : IGame
{
    public const string CorrectBulls = "BBBB,";
    public const int StartingGuesses = 0;
    public const int SecretCodeLength = 4;
    public static readonly string EmptyGuess = new string(' ', SecretCodeLength);

    private IGameStrategy gameStrategy;

    public Game(GameType gameType = GameType.Default)
    {
        gameStrategy = SetGameStrategy(GameStrategies.GetGameStrategy(gameType));
        SecretCode = gameStrategy.CreateSecretCode();
    }

    public string SecretCode { get; private set; } = EmptyGuess;
    public int NumberOfGuesses { get; private set; }

    public void SetSecretCode()
    {
        string code = gameStrategy.CreateSecretCode();
        if (int.TryParse(code, out int _) && code.Length == SecretCodeLength)
            SecretCode = code;
    }
    public void SetNumberOfGuesses(int numberOfGuesses = 0)
    {
        if (NumberOfGuesses >= 0)
            NumberOfGuesses = numberOfGuesses;
    }

    public IGameStrategy SetGameStrategy(IGameStrategy newStrategy)
    {
        gameStrategy = newStrategy;
        return gameStrategy;
    }

    public string EvaluateGuessForBullsAndCows(string code, string userGuess)
    {
        int cows = 0, bulls = 0;

        var codeCount = new Dictionary<char, int>();
        var guessCount = new Dictionary<char, int>();
        userGuess += EmptyGuess; // if player entered less than 4 chars

        for (int i = 0; i < SecretCodeLength; i++)
        {
            if (code[i] == userGuess[i])
            {
                bulls++;
                continue;
            }

            if (codeCount.ContainsKey(code[i]))
                codeCount[code[i]]++;
            else
                codeCount[code[i]] = 1;

            if (guessCount.ContainsKey(userGuess[i]))
                guessCount[userGuess[i]]++;
            else
                guessCount[userGuess[i]] = 1;
        }

        foreach (var entry in guessCount)
        {
            if (codeCount.TryGetValue(entry.Key, out int value))
                cows += Math.Min(entry.Value, value);
        }

        return new string('B', bulls) + "," + new string('C', cows);
    }
}