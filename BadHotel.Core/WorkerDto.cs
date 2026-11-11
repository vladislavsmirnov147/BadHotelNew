namespace BadHotel.Core;

public class WorkerDto
{
    public Guid Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Energy { get; set; }

    public string Group { get; set; } = string.Empty;
}
