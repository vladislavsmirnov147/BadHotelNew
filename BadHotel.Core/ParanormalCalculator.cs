
namespace BadHotel.Core;

public sealed class ParanormalCalculator
{
    public string Calculate(
        CalculationSnapshot snapshot,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        if (snapshot.Workload <= 0)
        {
            throw new ArgumentException(
                "Arvutuste maht peab olema suurem kui null.");
        }

        return snapshot.ObjectType switch
        {
            "Neetud tuba" => new NeetudTuba(
                snapshot.Name,
                snapshot.AnomalyLevel,
                snapshot.Parameter
            ).Arvuta(
                snapshot.Workload,
                cancellationToken),

            "Kummituslik lift" => new KummituslikLift(
                snapshot.Name,
                snapshot.Parameter,
                snapshot.AnomalyLevel
            ).Arvuta(
                snapshot.Workload,
                cancellationToken),

            "Neetud peegel" => new NeetudPeegel(
                snapshot.Name,
                snapshot.AnomalyLevel,
                snapshot.Parameter
            ).Arvuta(
                snapshot.Workload,
                cancellationToken),

            _ => throw new ArgumentException(
                $"Tundmatu objekti tüüp: {snapshot.ObjectType}")
        };
    }
}
