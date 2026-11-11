namespace BadHotel.Core.Persistence;

public class GroupEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<WorkerEntity> Workers { get; set; } = new();
}
