using System.Text.Json;

namespace BadHotel.Core.Persistence;

public class JsonWorldStorage : IWorldStorage
{
    public async Task SaveAsync(string filePath, WorldDto world)
    {
        ArgumentNullException.ThrowIfNull(world);

        await using FileStream stream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(
            stream,
            world,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
    }

    public async Task<WorldDto> LoadAsync(string filePath)
    {
        await using FileStream stream = File.OpenRead(filePath);

        WorldDto? world =
            await JsonSerializer.DeserializeAsync<WorldDto>(stream);

        return world
            ?? throw new InvalidDataException(
                "Maailma andmeid ei õnnestunud laadida.");
    }
}
