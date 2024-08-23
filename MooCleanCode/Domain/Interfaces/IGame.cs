namespace MooCleanCode.Domain.Interfaces;

public interface IGame
{
    public string Goal { get; }
    public int NumberOfGuesses { get; }

    public void SetGoal();
    public void SetNumberOfGuesses(int numberOfGuesses = 0);

    public IGameStrategy SetGameStrategy(IGameStrategy newStrategy);

    public string EvaluateGuessForBullsAndCows(string goal, string userGuess);
}