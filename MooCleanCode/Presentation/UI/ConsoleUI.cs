using MooCleanCode.Domain.Enums;
using MooCleanCode.Domain.Interfaces;
using MooCleanCode.Presentation.Interfaces;

namespace MooCleanCode.Presentation.UI;

public class ConsoleUI : IUI
{
    public void PutString(string s)
    {
        Console.WriteLine(s);
    }

    public string GetString()
    {
        return Console.ReadLine() ?? "";
    }

    public void ShowToplist(IEnumerable<IPlayer> toplist)
    {
        PutString("\nPlayer Games Average");
        foreach (var player in toplist)
        {
            PutString($"{player.Name,9}{player.GamesPlayed,5}{player.GetAverageGuessesPerGame(),9:F2}");
        }
    }

    public string GetUsername()
    {
        PutString("Enter your username: \n");
        string username = default;
        while (string.IsNullOrEmpty(username))
        {
            username = GetString();
        }
        return username;
    }

    public T MenuSelector<T>(List<T> meny, string header)
    {
        int selectorIndex = 0;
        while (true)
        {
            Console.Clear();
            DisplayMenuSelection<T>(meny.ToArray(), selectorIndex, header);
            var userInput = Console.ReadKey(true).Key;
            
            selectorIndex = userInput switch
            {
                ConsoleKey.UpArrow   => (selectorIndex - 1 + meny.Count) % meny.Count,
                ConsoleKey.DownArrow => (selectorIndex + 1 + meny.Count) % meny.Count,
                _ => selectorIndex
            };

            if (userInput == ConsoleKey.Enter)
                return meny[selectorIndex];
        }
    }

    private void DisplayMenuSelection<T>(Array meny, int selectorIndex, string header)
    {
        PutString(header);
        for (int i = 0; i < meny.Length; i++)
        {
            string selector = (i == selectorIndex) ? "->" : "  ";
            PutString($"{selector} {(T)meny.GetValue(i)}");
        }
    }
}