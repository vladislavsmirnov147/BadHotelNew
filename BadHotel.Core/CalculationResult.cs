namespace BadHotel.Core;

public sealed class CalculationResult
{
    public string ObjectName { get; }
    public string Status { get; }
    public string Result { get; }
    public long DurationMilliseconds { get; }

    public CalculationResult(
        string objectName,
        string status,
        string result,
        long durationMilliseconds)
    {
        ObjectName = objectName;
        Status = status;
        Result = result;
        DurationMilliseconds = durationMilliseconds;
    }
}
