using trade_compas.Interfaces.Helpers;

namespace trade_compas.Utils;

public class PathHelper(IWebHostEnvironment webHost) : IPathHelper
{
    public string GetCollectionPath(string collectionKey)
    {
        return Path.Combine(webHost.WebRootPath, $"collections/{collectionKey}.json");
    }

    public string GetImagePath(string fileName)
    {
        var uploadsPath = Path.Combine(webHost.WebRootPath, "uploads");

        return Path.Combine(uploadsPath, fileName);
    }
}