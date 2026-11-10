
namespace BadHotel.Core;

public class WorldStorageService
{
    private readonly IWorldStorage _jsonStorage;
    private readonly IWorldStorage _xmlStorage;
    private readonly IWorldStorage _sqlStorage;

    public WorldStorageService(
        IWorldStorage jsonStorage,
        IWorldStorage xmlStorage,
        IWorldStorage sqlStorage)
    {
        _jsonStorage = jsonStorage;
        _xmlStorage = xmlStorage;
        _sqlStorage = sqlStorage;
    }

    public async Task SaveAsync(
        string filePath,
        WorldDto world,
        CancellationToken token = default)
    {
        IWorldStorage storage = GetStorage(filePath);

        token.ThrowIfCancellationRequested();

        await storage.SaveAsync(filePath, world);
    }

    public async Task<WorldDto> LoadAsync(
        string filePath,
        CancellationToken token = default)
    {
        IWorldStorage storage = GetStorage(filePath);

        token.ThrowIfCancellationRequested();

        return await storage.LoadAsync(filePath);
    }

    private IWorldStorage GetStorage(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLowerInvariant();

        return extension switch
        {
            ".json" => _jsonStorage,
            ".xml" => _xmlStorage,
            ".db" => _sqlStorage,
            _ => throw new NotSupportedException(
                "Toetatud on ainult JSON-, XML- ja LocalDB-salvestus.")
        };
    }
}
