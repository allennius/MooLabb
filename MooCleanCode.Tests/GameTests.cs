using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Strategies;

namespace MooCleanCode.Tests;

[TestClass]
public class GameTests
{
    private Game game;

    [TestInitialize]
    public void InitializeTest()
    {
        game = new Game();
        game.SetGameStrategy(new MooGameStrategy());
    }
    
    [TestMethod]
    public void MooGameTest()
    {
        Assert.IsNotNull(game);
        Assert.IsTrue(game.Goal == Game.EmptyGuess);
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
        string uniqueResult = Game.CheckGuess(uniqueGoal, uniqueGuess);
        string result = Game.CheckGuess(goal, guess);
        
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
        string uniqueResult = Game.CheckGuess(uniqueGoal, uniqueGuess);
        string result = Game.CheckGuess(goal, guess);
        
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
    
        var goal = game.Goal;
        HashSet<int> mooGoal = [];
    
        foreach (var letter in goal)
        {
            Assert.IsTrue(int.TryParse(letter.ToString(), out int number));
            Assert.IsTrue(mooGoal.Add(letter));
        }
    }
    [TestMethod]
    public void MasterMindMakeGoalAndValidateNumbersInRangeTest()
    {
        game = new Game(GameType.MasterMindGame);
    
        var goal = game.Goal;
    
        foreach (var letter in goal)
        {
            Assert.IsTrue(int.TryParse(letter.ToString(), out int number));
            Assert.IsTrue(number is >= 0 and <= 5);
        }
    }
}
