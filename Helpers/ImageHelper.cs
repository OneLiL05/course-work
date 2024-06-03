using trade_compas.Interfaces.Helpers;

namespace trade_compas.Helpers;

public class ImageHelper(IPathHelper pathHelper) : IImageHelper
{
    public async Task<string> Create(IFormFile image)
    {
        var uniqueFileName = Path.GetFileNameWithoutExtension(image.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var imagePath = pathHelper.GetImagePath(uniqueFileName);

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return $"/uploads/{uniqueFileName}";
    }
}