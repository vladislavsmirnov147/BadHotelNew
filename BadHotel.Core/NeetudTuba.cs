namespace BadHotel.Core;

public sealed class NeetudTuba
{
    public string Nimi { get; }
    public int AnomaaliaTase { get; }
    public int Temperatuur { get; }

    public NeetudTuba(
        string nimi,
        int anomaaliaTase,
        int temperatuur)
    {
        Nimi = nimi;
        AnomaaliaTase = anomaaliaTase;
        Temperatuur = temperatuur;
    }

    public string Arvuta(
        int arvutusteMaht,
        CancellationToken tuhistamiseToken)
    {
        if (arvutusteMaht <= 0)
            throw new ArgumentException(
                "Arvutuste maht peab olema suurem kui null.");

        if (AnomaaliaTase < 1 || AnomaaliaTase > 10)
            throw new ArgumentException(
                "Anomaalia tase peab olema vahemikus 1 kuni 10.");

        long kummitusteAktiivsus = 0;
        long ohtlikudJuhtumid = 0;

        for (int i = 0; i < arvutusteMaht; i++)
        {
            tuhistamiseToken.ThrowIfCancellationRequested();

            long aktiivsus =
                ((long)i * AnomaaliaTase + Math.Abs(Temperatuur)) % 101;

            kummitusteAktiivsus += aktiivsus;

            if (aktiivsus > 75)
            {
                ohtlikudJuhtumid++;
            }
        }

        double keskmineAktiivsus =
            (double)kummitusteAktiivsus / arvutusteMaht;

        double ohuToenaosus =
            (double)ohtlikudJuhtumid / arvutusteMaht * 100;

        return
            $"Tuba: {Nimi}\n" +
            $"Keskmine kummituste aktiivsus: {keskmineAktiivsus:F2}\n" +
            $"Ohtlike juhtumite tõenäosus: {ohuToenaosus:F2}%";
    }
}
