namespace BadHotel.Core;

public sealed class KummituslikLift
{
    public string Nimi { get; }
    public int KorrusteArv { get; }
    public int AnomaaliaTase { get; }

    public KummituslikLift(
        string nimi,
        int korrusteArv,
        int anomaaliaTase)
    {
        Nimi = nimi;
        KorrusteArv = korrusteArv;
        AnomaaliaTase = anomaaliaTase;
    }

    public string Arvuta(
        int arvutusteMaht,
        CancellationToken tuhistamiseToken)
    {
        if (arvutusteMaht <= 0)
            throw new ArgumentException(
                "Arvutuste maht peab olema suurem kui null.");

        if (KorrusteArv < 2)
            throw new ArgumentException(
                "Hoones peab olema vähemalt kaks korrust.");

        if (AnomaaliaTase < 1 || AnomaaliaTase > 10)
            throw new ArgumentException(
                "Anomaalia tase peab olema vahemikus 1 kuni 10.");

        long parimOhutase = long.MaxValue;
        long halvimOhutase = long.MinValue;

        int parimMarsruut = 0;
        int halvimMarsruut = 0;

        for (int i = 0; i < arvutusteMaht; i++)
        {
            tuhistamiseToken.ThrowIfCancellationRequested();

            int algusKorrus = i % KorrusteArv + 1;
            int loppKorrus = (i * 7 + 3) % KorrusteArv + 1;

            long vahemaa = Math.Abs(
                (long)loppKorrus - algusKorrus);

            long ohutase =
                (vahemaa * AnomaaliaTase + i % 97) % 1000;

            if (ohutase < parimOhutase)
            {
                parimOhutase = ohutase;
                parimMarsruut = i + 1;
            }

            if (ohutase > halvimOhutase)
            {
                halvimOhutase = ohutase;
                halvimMarsruut = i + 1;
            }
        }

        return
            $"Lift: {Nimi}\n" +
            $"Korruste arv: {KorrusteArv}\n" +
            $"Kõige ohutum marsruut: {parimMarsruut}\n" +
            $"Väikseim ohutase: {parimOhutase}\n" +
            $"Kõige ohtlikum marsruut: {halvimMarsruut}\n" +
            $"Suurim ohutase: {halvimOhutase}";
    }
}
