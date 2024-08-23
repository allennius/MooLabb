using MooCleanCode.Application;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Infrastructure.Repositories;

namespace MooCleanCode.Tests;

[TestClass]
public class MooGameServiceTests
{
    private GameService gameservice;

    [TestInitialize]
    public void InitializeTest()
    {
        gameservice = new GameService(new Game(), new MockGameRepository());
        gameservice.SetGameMode(GameType.Default);
        gameservice.ResetGame();
    }

    [TestMethod]
    public void GameServiceIsInitializedTest()
    {
        Assert.IsNotNull(gameservice);
        Assert.IsTrue(gameservice.GetGoal().Length == 4);
        Assert.IsTrue(gameservice.GetNumberOfGuesses() == 0);
    }

    [TestMethod]
    public void CheckGuessIncrementsGuesses()
    {
        int startingGuesses = gameservice.GetNumberOfGuesses();
        gameservice.HandleGuess("1235");
        int afterGuessGuesses = gameservice.GetNumberOfGuesses();

        Assert.AreEqual(startingGuesses + 1, afterGuessGuesses);
    }

    [TestMethod]
    public void CheckMakeGoalAndResetNumberOfGuessesNewGameTest()
    {
        string goal = gameservice.GetGoal();

        // increment guesses
        gameservice.HandleGuess("1111");
        int numberOfGuesses = gameservice.GetNumberOfGuesses();

        gameservice.ResetGame();

        Assert.IsTrue(goal.Length == 4);
        Assert.IsTrue(goal is string);
        Assert.IsTrue(numberOfGuesses > 0);
        Assert.IsTrue(gameservice.GetNumberOfGuesses() == 0);
        Assert.AreNotEqual(goal, gameservice.GetGoal());
    }

    [TestMethod]
    public void AddGameToToplistIfNumberOfGuessesIsCorrect()
    {
        var toplist = gameservice.GetToplist();
        // failing - no guesses recorded
        gameservice.AddGameToToplist("Windu");
        Assert.AreEqual(toplist.Count(), gameservice.GetToplist().Count());

        // success after adding guess.
        gameservice.HandleGuess("2222");
        gameservice.AddGameToToplist("Windu");
        int guesses = gameservice.GetNumberOfGuesses();

        Assert.AreNotEqual(toplist.Count(), gameservice.GetToplist().Count());
        Assert.IsTrue(gameservice.GetToplist().Any(p => p.Name == "Windu"));
    }

    [TestMethod]
    public void GetToplistIsSortedAndPlayerIsUniqueTest()
    {
        const string playerName = "Yoda";
        gameservice.HandleGuess("1233");
        gameservice.AddGameToToplist(playerName);
        gameservice.AddGameToToplist(playerName);

        var toplist = gameservice.GetToplist().ToList();
        int playerNameCount = toplist.Count(p => p.Name == playerName);

        for (int i = 1; i < toplist.Count; i++)
            Assert.IsTrue(toplist[i].GetAverageGuessesPerGame() >= toplist[i - 1].GetAverageGuessesPerGame());

        Assert.IsTrue(playerNameCount == 1);
    }
}