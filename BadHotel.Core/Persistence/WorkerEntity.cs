namespace BadHotel.Core.Persistence;

public class WorkerEntity
{
    public Guid Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Energy { get; set; }

    public int GroupId { get; set; }

    public GroupEntity Group { get; set; } = null!;
}

public class ReceptionistEntity : WorkerEntity
{
}

public class CleanerEntity : WorkerEntity
{
}

public class CookEntity : WorkerEntity
{
}
