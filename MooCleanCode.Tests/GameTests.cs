using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;
using MooCleanCode.Domain.Strategies;

namespace MooCleanCode.Tests;

[TestClass]
public class GameTests
{
    private IGame game;

    [TestInitialize]
    public void InitializeTest()
    {
        game = new Game();
        game.SetGameStrategy(new MooGameStrategy());
    }

    [TestMethod]
    public void GameIsInitiatedTest()
    {
        Assert.IsNotNull(game);
        Assert.IsTrue(game.NumberOfGuesses == 0);
    }


    [TestMethod]
    public void CheckGuessIncorrectGuessTest()
    {
        string uniqueGoal = "1234";
        string goal = "1221";
        string uniqueGuess = "1243";
        string guess = "1212";

        string excpectedResult = "BB,CC";
        string uniqueResult = game.EvaluateGuessForBullsAndCows(uniqueGoal, uniqueGuess);
        string result = game.EvaluateGuessForBullsAndCows(goal, guess);

        Assert.AreEqual(excpectedResult, uniqueResult);
        Assert.AreEqual(excpectedResult, result);
    }

    [TestMethod]
    public void CheckGuessCorrectGuessTest()
    {
        string uniqueGoal = "1234";
        string goal = "1111";
        string uniqueGuess = "1234";
        string guess = "1111";

        string excpectedResult = "BBBB,";
        string uniqueResult = game.EvaluateGuessForBullsAndCows(uniqueGoal, uniqueGuess);
        string result = game.EvaluateGuessForBullsAndCows(goal, guess);

        Assert.AreEqual(excpectedResult, uniqueResult);
        Assert.AreEqual(excpectedResult, result);
    }

    [TestMethod]
    public void IncrementNumberOfGuessesWithParameterTest()
    {
        const int increment = 2;

        Assert.AreEqual(game.NumberOfGuesses, Game.StartingGuesses);

        game.SetNumberOfGuesses(increment);
        Assert.AreEqual(game.NumberOfGuesses, Game.StartingGuesses + increment);
    }

    [TestMethod]
    public void SetGameStrategyTest()
    {
        var mooGameStrategy = game.SetGameStrategy(new MooGameStrategy());
        var masterMindGameStrategy = game.SetGameStrategy(new MasterMindGameStrategy());

        Assert.AreEqual(mooGameStrategy.GetType(), typeof(MooGameStrategy));
        Assert.AreEqual(masterMindGameStrategy.GetType(), typeof(MasterMindGameStrategy));
    }
    [TestMethod]
    public void MooGameMakeGoalAndValidateNumbersUniqueTest()
    {
        game.SetGameStrategy(new MooGameStrategy());

        string goal = game.Goal;
        HashSet<int> mooGoal = [];

        foreach (char letter in goal)
        {
            Assert.IsTrue(int.TryParse(letter.ToString(), out int number));
            Assert.IsTrue(mooGoal.Add(letter));
        }
    }
    [TestMethod]
    public void MasterMindMakeGoalAndValidateNumbersInRangeTest()
    {
        game = new Game(GameType.MasterMindGame);

        string goal = game.Goal;

        foreach (char letter in goal)
        {
            Assert.IsTrue(int.TryParse(letter.ToString(), out int number));
            Assert.IsTrue(number is >= 0 and <= 5);
        }
    }
}