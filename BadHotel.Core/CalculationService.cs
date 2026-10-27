
using System.Diagnostics;

namespace BadHotel.Core;

public sealed class CalculationService
{
    private readonly ParanormalCalculator _arvutaja = new();

    public async Task<List<CalculationResult>> RunSequentialAsync(
        IReadOnlyList<CalculationSnapshot> snapshots,
        CancellationToken cancellationToken)
    {
        List<CalculationResult> tulemused = new();

        foreach (CalculationSnapshot snapshot in snapshots)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                LisaTuhistatudTulemused(snapshots, tulemused);
                break;
            }

            Stopwatch stopper = Stopwatch.StartNew();

            try
            {
                string tulemus = await Task.Run(
                    () => _arvutaja.Calculate(snapshot, cancellationToken),
                    cancellationToken);

                stopper.Stop();

                tulemused.Add(new CalculationResult(
                    snapshot.Name,
                    "Õnnestus",
                    tulemus,
                    stopper.ElapsedMilliseconds));
            }
            catch (OperationCanceledException)
            {
                stopper.Stop();

                tulemused.Add(new CalculationResult(
                    snapshot.Name,
                    "Tühistatud",
                    "Arvutus tühistati.",
                    stopper.ElapsedMilliseconds));

                LisaTuhistatudTulemused(snapshots, tulemused);
                break;
            }
            catch (Exception ex)
            {
                stopper.Stop();

                tulemused.Add(new CalculationResult(
                    snapshot.Name,
                    "Viga",
                    ex.Message,
                    stopper.ElapsedMilliseconds));
            }
        }

        return tulemused;
    }

    public async Task<List<CalculationResult>> RunParallelAsync(
        IReadOnlyList<CalculationSnapshot> snapshots,
        CancellationToken cancellationToken)
    {
        Task<CalculationResult>[] ülesanded = snapshots
            .Select(snapshot => ArvutaUksAsync(snapshot, cancellationToken))
            .ToArray();

        return (await Task.WhenAll(ülesanded)).ToList();
    }

    private async Task<CalculationResult> ArvutaUksAsync(
        CalculationSnapshot snapshot,
        CancellationToken cancellationToken)
    {
        Stopwatch stopper = Stopwatch.StartNew();

        try
        {
            string tulemus = await Task.Run(
                () => _arvutaja.Calculate(snapshot, cancellationToken),
                cancellationToken);

            stopper.Stop();

            return new CalculationResult(
                snapshot.Name,
                "Õnnestus",
                tulemus,
                stopper.ElapsedMilliseconds);
        }
        catch (OperationCanceledException)
        {
            stopper.Stop();

            return new CalculationResult(
                snapshot.Name,
                "Tühistatud",
                "Arvutus tühistati.",
                stopper.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopper.Stop();

            return new CalculationResult(
                snapshot.Name,
                "Viga",
                ex.Message,
                stopper.ElapsedMilliseconds);
        }
    }

    private static void LisaTuhistatudTulemused(
        IReadOnlyList<CalculationSnapshot> snapshots,
        List<CalculationResult> tulemused)
    {
        foreach (CalculationSnapshot snapshot in snapshots)
        {
            if (!tulemused.Any(tulemus => tulemus.ObjectName == snapshot.Name))
            {
                tulemused.Add(new CalculationResult(
                    snapshot.Name,
                    "Tühistatud",
                    "Arvutust ei käivitatud tühistamise tõttu.",
                    0));
            }
        }
    }
}
