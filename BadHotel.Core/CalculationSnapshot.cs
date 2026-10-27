namespace BadHotel.Core;

public sealed class CalculationSnapshot
{
    public string Name { get; }
    public string ObjectType { get; }
    public int Workload { get; }
    public int AnomalyLevel { get; }
    public int Parameter { get; }

    public CalculationSnapshot(
        string name,
        string objectType,
        int workload,
        int anomalyLevel,
        int parameter)
    {
        Name = name;
        ObjectType = objectType;
        Workload = workload;
        AnomalyLevel = anomalyLevel;
        Parameter = parameter;
    }
}
