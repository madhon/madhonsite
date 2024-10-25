namespace Romulus.Web;

using System.Text.Json;

#pragma warning disable MA0048
public record BuildInfo(string BranchName, string BuildNumber, string BuildId, string CommitHash);
#pragma warning restore MA0048

public static class AppVersionInfo
{
    private const string _buildFileName = "buildinfo.json";
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "MA0011:IFormatProvider is missing", Justification = "Build versioning")]
    private static BuildInfo _fileBuildInfo = new(
        BranchName: "",
        BuildNumber: DateTime.UtcNow.ToString("yyyyMMdd") + ".0",
        BuildId: "xxxxxx",
        CommitHash: $"Not yet initialised - call {nameof(InitialiseBuildInfoGivenPath)}"
    );

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "MA0011:IFormatProvider is missing", Justification = "Build versioning")]
    public static void InitialiseBuildInfoGivenPath(string path)
    {
        var buildFilePath = Path.Combine(path, _buildFileName);
        if (File.Exists(buildFilePath))
        {
            try
            {
                var buildInfoJson = File.ReadAllText(buildFilePath);
                var buildInfo = JsonSerializer.Deserialize<BuildInfo>(buildInfoJson, AppJsonSerializerContext.Default.BuildInfo);
                _fileBuildInfo = buildInfo ?? throw new Exception($"Failed to deserialise {_buildFileName}");
            }
            catch (Exception)
            {
                _fileBuildInfo = new BuildInfo(
                    BranchName: "",
                    BuildNumber: DateTime.UtcNow.ToString("yyyyMMdd") + ".0",
                    BuildId: "xxxxxx",
                    CommitHash: "Failed to load build info from buildinfo.json"
                );
            }
        }
    }

    public static BuildInfo GetBuildInfo() => _fileBuildInfo;
}
