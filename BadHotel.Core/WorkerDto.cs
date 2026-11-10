namespace BadHotel.Core;

public class WorkerDto
{
    public Guid Id { get; set; }

    public string Type { get; set; } = "";

    public string Name { get; set; } = "";

    public int Energy { get; set; }

    public string Group { get; set; } = "";
}