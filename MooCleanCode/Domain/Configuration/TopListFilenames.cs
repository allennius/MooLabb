using System.Net.Mime;
using MooCleanCode.Domain.Enums;

namespace MooCleanCode.Domain.Configuration;

public static class TopListFilenames
{
    static private readonly string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "highScores");
    private const string defaultFilename = "mooResults.txt";
    static private readonly Dictionary<GameType, string> Filenames = new ()
    {
        { GameType.Default , defaultFilename },
        { GameType.MooGame, defaultFilename },
        { GameType.MasterMindGame, "masterMindGameResults.txt" }
    };

    public static string GetFileName(GameType gameType)
    {
        string filename = Filenames.GetValueOrDefault(gameType, defaultFilename);
        Directory.CreateDirectory(folderPath);
        return Path.Combine(folderPath, filename);
    }
}