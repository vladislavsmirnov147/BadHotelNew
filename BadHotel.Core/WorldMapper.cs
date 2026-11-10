
namespace BadHotel.Core;

public static class WorldMapper
{
    public static WorldDto ToDto(IEnumerable<HotelWorker> workers)
    {
        WorldDto world = new();

        foreach (HotelWorker worker in workers)
        {
            WorkerDto dto = new()
            {
                Id = worker.Id,
                Type = worker.GetType().Name,
                Name = worker.Name,
                Energy = worker.Energy,
                Group = worker.Group
            };

            world.Workers.Add(dto);
        }

        return world;
    }

    public static List<HotelWorker> FromDto(WorldDto world)
    {
        if (world?.Workers == null)
        {
            throw new InvalidDataException(
                "Maailma andmed puuduvad.");
        }

        List<HotelWorker> workers = new();
        HashSet<Guid> ids = new();

        foreach (WorkerDto dto in world.Workers)
        {
            if (dto == null)
            {
                throw new InvalidDataException(
                    "Töötaja andmed puuduvad.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new InvalidDataException(
                    "Töötaja nimi puudub.");
            }

            if (string.IsNullOrWhiteSpace(dto.Group))
            {
                throw new InvalidDataException(
                    "Töötaja grupp puudub.");
            }

            if (dto.Id == Guid.Empty)
            {
                throw new InvalidDataException(
                    "Töötaja ID ei tohi olla tühi.");
            }

            if (!ids.Add(dto.Id))
            {
                throw new InvalidDataException(
                    "Töötajate ID-d peavad olema erinevad.");
            }

            if (dto.Energy < 0 || dto.Energy > 100)
            {
                throw new InvalidDataException(
                    "Töötaja energia peab olema vahemikus 0 kuni 100.");
            }

            HotelWorker worker = dto.Type switch
            {
                "Receptionist" => new Receptionist(dto.Name),
                "Cleaner" => new Cleaner(dto.Name),
                "Cook" => new Cook(dto.Name),
                _ => throw new InvalidDataException(
                    $"Tundmatu töötaja tüüp: {dto.Type}")
            };

            worker.RestoreState(dto.Id, dto.Energy);
            worker.Group = dto.Group;

            workers.Add(worker);
        }

        return workers;
    }
}
