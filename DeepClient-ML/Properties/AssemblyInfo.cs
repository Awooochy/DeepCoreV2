using MelonLoader;

[assembly: MelonInfo(typeof(DeepCore.Entry), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame(null, null)]
public static class BuildInfo
{
    public const string Name = "DeepClient";
    public const string Description = "Man this guy amanishe.";
    public const string Author = "- Biscuit & Awooochy -";
    public const string Company = null;
    public const string Version = "1.0";
    public const string DownloadLink = null;
}