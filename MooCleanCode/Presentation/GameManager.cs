using MooCleanCode.Application.Interfaces;
using MooCleanCode.Domain.Configuration;
using MooCleanCode.Domain.Entities;
using MooCleanCode.Domain.Enums;
using MooCleanCode.Presentation.Interfaces;

namespace MooCleanCode.Presentation;

public class GameManager(IGameService gameService, IUI ui) : IGameManager
{
    public void Run()
    {
        var gameSelection = ui.MenuSelector(
            Enum.GetValues(typeof(GameType)).Cast<GameType>().ToList(),
            "Which game would you like to play? (Use arrow keys and enter)");

        gameService.SetGameMode(gameSelection);

        string name = ui.GetUsername();

        do
        {
            NewGame();
            ui.PutString($"{gameSelection.ToString()} {GameStrategies.GetGameRules(gameSelection)}");
            PlayGame(name);
        } while (PlayOn());
    }

    private void PlayGame(string name)
    {
        string userGuess = Game.EmptyGuess;
        string cowsAndBulls = Game.EmptyGuess;
        do
        {
            userGuess = ui.GetString();
            cowsAndBulls = gameService.HandleGuess(userGuess);
            ui.PutString(cowsAndBulls);
        } while (cowsAndBulls != Game.CorrectBulls);

        gameService.AddGameToToplist(name);
        ui.ShowToplist(gameService.GetToplist());
        ui.PutString($"\nGood job {name} this round took {gameService.GetNumberOfGuesses()} guesses. \n" +
                     $"\npress any key to continue");
        ui.GetString();
    }

    private void NewGame()
    {
        gameService.ResetGame();
        ui.PutString("New Game: \n");

        //comment out or remove next line to play real games!
        ui.PutString("For practice, Number is: " + gameService.GetSecretCode() + "\n");
    }

    private bool PlayOn()
    {
        string answer = ui.MenuSelector<string>(["Yes", "No"], "Play again?");

        return string.IsNullOrEmpty(answer) || answer[..1].ToLower() != "n";
    }
}