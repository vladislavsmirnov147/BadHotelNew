using System.Text.Json;

namespace BadHotel.Core;

public class JsonWorldStorage : IWorldStorage
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public async Task SaveAsync(string filePath, WorldDto world)
    {
        string json = JsonSerializer.Serialize(world, _options);

        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<WorldDto> LoadAsync(string filePath)
    {
        string json = await File.ReadAllTextAsync(filePath);

        WorldDto? world = JsonSerializer.Deserialize<WorldDto>(json, _options);

        if (world == null)
        {
            throw new InvalidDataException("Maailma andmeid ei õnnestunud laadida.");
        }

        return world;
    }
}