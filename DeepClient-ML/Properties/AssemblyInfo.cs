using MelonLoader;

[assembly: MelonInfo(typeof(DeepCore.Entry), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame(null, null)]
public static class BuildInfo
{
    public const string Name = "DeepCore V2";
    public const string Description = "Lets fix this mess.";
    public const string Author = "- Awooochy -";
    public const string Company = null;
    public const string Version = "1.0";
    public const string DownloadLink = null;
}