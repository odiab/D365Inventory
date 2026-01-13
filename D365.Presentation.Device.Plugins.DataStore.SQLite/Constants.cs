namespace D365.Presentation.Device.Plugins.DataStore.SQLite;

// All the code in this file is included in all platforms.
internal static class Constants
{
    public const string DatabaseFileName = "D365LocalDb.db3";

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
}