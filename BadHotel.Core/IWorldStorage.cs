namespace BadHotel.Core;

public interface IWorldStorage
{
    Task SaveAsync(string filePath, WorldDto world);

    Task<WorldDto> LoadAsync(string filePath);
}