using System.Text;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;

namespace MooCleanCode.Domain.Entities;

public class Game
{
    public const string CorrectBulls = "BBBB,";
    public const int StartingGuesses = 0;
    public static readonly string EmptyGuess = new string(' ', 4);

    public string Goal { get; private set; } = EmptyGuess;
    public int NumberOfGuesses { get; private set; }

    private IGameStrategy gameStrategy;

    public Game(GameType gameType = GameType.Default)
    {
        gameStrategy = SetGameStrategy(GameStrategies.GetGameStrategy(gameType));
        Goal = gameStrategy.MakeGoal();
    }
    public void SetGoal()
    {   
        string goal = gameStrategy.MakeGoal();
        if (int.TryParse(goal, out int _) && goal.Length == 4)
            Goal = goal;
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

    public static string EvaluateGuessForBullsAndCows(string goal, string userGuess)
    {
        int cows = 0, bulls = 0;

        Dictionary<char, int> goalCount = new();
        Dictionary<char, int> guessCount = new();
        userGuess += EmptyGuess; // if player entered less than 4 chars

        for (int i = 0; i < 4; i++)
        {
            if (goal[i] == userGuess[i])
            {
                bulls++;
                continue;
            }

            if (goalCount.ContainsKey(goal[i]))
                goalCount[goal[i]]++;
            else
                goalCount[goal[i]] = 1;

            if (guessCount.ContainsKey(userGuess[i]))
                guessCount[userGuess[i]]++;
            else
                guessCount[userGuess[i]] = 1;
        }

        foreach (var entry in guessCount)
        {
            if (goalCount.TryGetValue(entry.Key, out int value))
                cows += Math.Min(entry.Value, value);
        }

        return new string('B', bulls) + "," + new string('C', cows);
    }
}