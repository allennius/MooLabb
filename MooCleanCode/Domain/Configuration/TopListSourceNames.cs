using MooCleanCode.Domain.Enums;

namespace MooCleanCode.Domain.Configuration;

public static class TopListSourceNames
{
    private const string defaultFilename = "mooResults";

    static private readonly string folderPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "highScores");

    static private readonly Dictionary<GameType, string> Filenames = new Dictionary<GameType, string>
    {
        { GameType.Default, defaultFilename },
        { GameType.MooGame, defaultFilename },
        { GameType.MasterMindGame, "masterMindGameResults" }
    };

    public static string GetFileName(GameType gameType)
    {
        string filename = Filenames.GetValueOrDefault(gameType, defaultFilename);
        Directory.CreateDirectory(folderPath);
        return Path.Combine(folderPath, filename);
    }
    public static string GetCollectionName(GameType gameType)
    {
        string filename = Filenames.GetValueOrDefault(gameType, defaultFilename);
        return filename;
    }
}