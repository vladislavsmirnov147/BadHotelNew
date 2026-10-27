namespace BadHotel.Core;

public sealed class NeetudPeegel
{
    public string Nimi { get; }
    public int AnomaaliaTase { get; }
    public int EnnustusePikkus { get; }

    public NeetudPeegel(
        string nimi,
        int anomaaliaTase,
        int ennustusePikkus)
    {
        Nimi = nimi;
        AnomaaliaTase = anomaaliaTase;
        EnnustusePikkus = ennustusePikkus;
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

        if (EnnustusePikkus <= 0)
            throw new ArgumentException(
                "Ennustuse pikkus peab olema suurem kui null.");

        long headTulevikud = 0;
        long halvadTulevikud = 0;
        long katastroofid = 0;

        for (int i = 0; i < arvutusteMaht; i++)
        {
            tuhistamiseToken.ThrowIfCancellationRequested();

            long tulevikuOht = 0;

            for (int paev = 0; paev < EnnustusePikkus; paev++)
            {
                tuhistamiseToken.ThrowIfCancellationRequested();

                long sundmus =
                    ((long)i * 17 + paev * 31 + AnomaaliaTase * 13) % 100;

                tulevikuOht += sundmus;
            }

            double keskmineOht =
                (double)tulevikuOht / EnnustusePikkus;

            if (keskmineOht < 40)
            {
                headTulevikud++;
            }
            else if (keskmineOht < 65)
            {
                halvadTulevikud++;
            }
            else
            {
                katastroofid++;
            }
        }

        double katastroofiToenaosus =
            (double)katastroofid / arvutusteMaht * 100;

        string koigeToenaolisemTulevik;

        if (headTulevikud >= halvadTulevikud &&
            headTulevikud >= katastroofid)
        {
            koigeToenaolisemTulevik = "Hea tulevik";
        }
        else if (halvadTulevikud >= katastroofid)
        {
            koigeToenaolisemTulevik = "Halb tulevik";
        }
        else
        {
            koigeToenaolisemTulevik = "Katastroof";
        }

        return
            $"Peegel: {Nimi}\n" +
            $"Ennustuse pikkus: {EnnustusePikkus} päeva\n" +
            $"Head tulevikud: {headTulevikud}\n" +
            $"Halvad tulevikud: {halvadTulevikud}\n" +
            $"Katastroofid: {katastroofid}\n" +
            $"Kõige tõenäolisem tulevik: {koigeToenaolisemTulevik}\n" +
            $"Katastroofi tõenäosus: {katastroofiToenaosus:F2}%";
    }
}
