namespace BadHotel.Core;

public static class WorkerMapper
{
    public static WorkerDto ToDto(HotelWorker worker, string group)
    {
        ArgumentNullException.ThrowIfNull(worker);

        return new WorkerDto
        {
            Id = worker.Id,
            Type = worker.GetType().Name,
            Name = worker.Name,
            Energy = worker.Energy,
            Group = worker.Group
        };
    }

    public static HotelWorker FromDto(WorkerDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        HotelWorker worker = dto.Type switch
        {
            nameof(Receptionist) => new Receptionist(dto.Name),
            nameof(Cleaner) => new Cleaner(dto.Name),
            nameof(Cook) => new Cook(dto.Name),
            _ => throw new ArgumentException(
                $"Tundmatu töötaja tüüp: {dto.Type}")
        };

        worker.RestoreState(dto.Id, dto.Energy);
        worker.Group = dto.Group;

        return worker;
    }
}
