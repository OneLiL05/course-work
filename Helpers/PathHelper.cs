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
        // var uploadsFolder = Path.Combine(webHost.WebRootPath, "uploads");
        //
        // if (!Directory.Exists(uploadsFolder))
        // {
        //     Directory.CreateDirectory(uploadsFolder);
        // }

        return Path.Combine(webHost.WebRootPath, $"uploads/{fileName}.webp");
    }
}