namespace trade_compas.Interfaces.Helpers;

public interface IImageHelper
{
    Task<string> Create(IFormFile image);
}