using System.Xml.Serialization;

namespace BadHotel.Core;

public class XmlWorldStorage : IWorldStorage
{
    private readonly XmlSerializer _serializer = new(typeof(WorldDto));

    public async Task SaveAsync(string filePath, WorldDto world)
    {
        await using FileStream stream = new(filePath, FileMode.Create);

        _serializer.Serialize(stream, world);

        await stream.FlushAsync();
    }

    public async Task<WorldDto> LoadAsync(string filePath)
    {
        await using FileStream stream = new(filePath, FileMode.Open);

        WorldDto? world = _serializer.Deserialize(stream) as WorldDto;

        if (world == null)
        {
            throw new InvalidDataException("XML-andmeid ei õnnestunud laadida.");
        }

        return world;
    }
}