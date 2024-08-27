namespace MooCleanCode.Domain.Interfaces;

public interface IGame
{
    public string SecretCode { get; }
    public int NumberOfGuesses { get; }

    public void SetSecretCode();
    public void SetNumberOfGuesses(int numberOfGuesses = 0);

    public IGameStrategy SetGameStrategy(IGameStrategy newStrategy);

    public string EvaluateGuessForBullsAndCows(string code, string userGuess);
}