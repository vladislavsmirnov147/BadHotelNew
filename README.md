BadHotelNew - lühike käivitusjuhend



Minu projekt on BadHotelNew ja see on üks päris hull hotell! Alguses oli siin ainult töötajate süsteem, aga nüüd saab programmis isegi paranormaalseid arvutusi teha.



Programmi käivitamiseks tuleb avada BadHotelNew projekt Visual Studio 2026 programmis. Seejärel tuleb valida BadHotel.Wpf käivitusprojektiks ja vajutada F5. Pärast seda avaneb hotelli aken.



Programmis saab lisada erinevaid töötajaid, näiteks vastuvõtutöötaja, koristaja ja kokk. Neid saab valida, tööle panna, kasutada nende erivõimeid ja käivitada hullumeelseid tegevusi. Lisaks saab töötajaid otsida, sorteerida ja eemaldada. Kõige huvitavam osa on aga paranormaalsed arvutused! Siin on kolm objekti: Neetud tuba, Kummituslik lift ja Neetud peegel. Saan valida ühe või mitu objekti ning määrata arvutuste mahu, näiteks 10000000. Seejärel saan valida kahe arvutusrežiimi vahel: Järjestikune ja Paralleelne. Kui vajutan Alusta arvutust, hakkab programm valitud objektidega arvutusi tegema. Kõige üllatavam on see, et programmi aken jääb arvutamise ajal kasutatavaks! Pärast arvutamist näitab programm iga objekti kohta eraldi objekti nime, lõppolekut, tulemust või veateadet ning arvutamise kestust millisekundites.



1)Neetud tuba:

Objekt: Tuba 13 (Neetud tuba)

Olek: Õnnestus

Tulemus: Keskmine kummituste aktiivsus on 50.00 ja ohtlike juhtumite tõenäosus on 24.75%.

Tuba: Tuba 13

Keskmine kummituste aktiivsus: 50.00

Ohtlike juhtumite tõenäosus: 24.75%

Kestus: sõltub käivitamisest (kuvatakse millisekundites)



2)Kummituslik lift:

Objekt: Hotelli lift

Olek: Õnnestus

Tulemus: Kõige ohutum marsruut on 1 ja kõige ohtlikum marsruut on 194.

Korruste arv: 12

Kõige ohutum marsruut: 1

Väikseim ohutase: 24

Kõige ohtlikum marsruut: 194

Suurim ohutase: 168

Kestus: sõltub käivitamisest (kuvatakse millisekundites)



3)Neetud peegel:

Objekt: Vana peegel

Olek: Õnnestus

Tulemus: Kõige tõenäolisem tulevik on halb tulevik. Programm arvutab erinevaid tulevikuvõimalusi ja hindab katastroofi tõenäosust.

Peegel: Vana peegel

Ennustuse pikkus: 25 päeva

Head tulevikud: 0

Halvad tulevikud: 10000000

Katastroofid: 0

Kõige tõenäolisem tulevik: Halb tulevik

Katastroofi tõenäosus: 0.00%

Kestus: sõltub käivitamisest (kuvatakse millisekundites)





Vea test:

Objekt: Hotelli lift

Olek: Viga

Tulemus: Hoones peab olema vähemalt kaks korrust.

Kestus: 1 ms





Režiim: Järjestikune. Koguaeg: 1432 ms





Lõppolek võib olla Õnnestus, Viga või Tühistatud. Lisaks kuvatakse kogu töö aeg ehk aeg, mis kulus kõikide valitud objektide arvutuste tegemiseks. Nii saan kohe aru, milline objekt töötas edukalt, millisel tekkis viga ja kui kaua kõik aega võttis. Kahe režiimi võrdlemiseks käivitan samad objektid sama arvutuste mahuga kõigepealt järjestikku ja seejärel paralleelselt. Seejärel võrdlen kogu töö aega. Mind üllatas, et sama ülesande täitmise aeg võib eri režiimides olla erinev. Programmis saab arvutusi ka tühistada. Selle kontrollimiseks sisestan suurema arvutuste mahu, näiteks 100000000, vajutan Alusta arvutust ja seejärel Tühista. Tühistatud objektide lõppolekuks kuvatakse Tühistatud. Väga huvitav on see, et programm oskab arvutamise katkestada ja näitab ikkagi, mis objektidega juhtus.



Veel üks põnev asi on vigade kontrollimine. Selleks märgin Vea test (lifti vea kontroll) ja käivitan arvutused. Testimise ajal saab lift vale parameetri, sest sellel on ainult üks korrus. Lifti lõppolekuks kuvatakse Viga ja tulemuse asemel näen veateadet. Samal ajal jätkavad Neetud tuba ja Neetud peegel oma arvutusi ning nende lõppolekuks kuvatakse Õnnestus. See üllatas mind eriti, sest ühe objekti viga ei peata kogu programm - iga objekti kohta näen ikkagi selle nime, lõppolekut, tulemust või veateadet ja kestust. Lisaks näen kogu töö aega.



Pärast vea kontrollimist eemaldan märgistuse Vea test juurest ja lift töötab jälle tavaliselt.



Lõpuks saan oma BadHotelNew programmis hallata hotelli töötajaid, teha paranormaalseid arvutusi, võrrelda järjestikust ja paralleelset režiimi, tühistada arvutusi ning kontrollida vigade käsitlemist. Alguses tundus see kõik väga keeruline, aga lõpuks sain selle tööle. Ma ei oleks arvanud, et minu hotelliprogramm suudab nii palju teha.





Vol.4 ajal tegelesin sellega, et minu BadHotelNew programm oskaks nüüd ka töötajate andmeid salvestada. Varem sain küll töötajaid lisada ja nendega erinevaid tegevusi teha, aga pärast programmi sulgemist läksid kõik andmed kaduma. Nüüd saan kogu hotelli maailma salvestada ja hiljem uuesti avada. Salvestamisel jäävad alles töötaja nimi, tüüp, energia, grupp ja tema unikaalne GUID.

Kõigepealt õppisin, kuidas salvestada andmeid JSON- ja XML-failidesse. Programmi lisandusid nupud Salvesta maailm ja Laadi maailm. Esimese nupuga saan valida, kuhu ja millises formaadis oma andmed salvestan. Teise nupuga saan need jälle programmi tagasi tuua. Selle jaoks tegin mitu uut klassi, näiteks JsonWorldStorage, XmlWorldStorage, WorkerDto, WorldDto ja WorldMapper. Alguses oli päris keeruline aru saada, milleks neid kõiki vaja on, aga lõpuks sain aru, et iga klass teeb oma osa tööst. Kasutasin ka IWorldStorage liidest ja WorldStorageService klassi, et programm oskaks erinevaid salvestusviise kasutada.

Proovisin ka seda, mis juhtub siis, kui salvestatud failis on midagi valesti. Muutsin JSON-failis ühe töötaja GUID-i meelega valeks ja proovisin seda faili laadida. Programm näitas veateadet, aga õnneks ei kadunud juba olemasolevad töötajad ära. Minu arvates on see väga oluline, sest üks vigane fail ei tohiks kogu programmi andmeid ära rikkuda.

Järgmisena hakkasin tegelema andmebaasiga. See osa tundus mulle alguses kõige keerulisem, sest ma polnud varem LocalDB-ga eriti tegelenud. Kasutasin SQL Server Express LocalDB-d ja lõin andmebaasi nimega BadHotelDb. Sinna tegin kaks tabelit: Workers ja Groups. Workers tabelis hoitakse töötajate andmeid ning Groups tabelis nende gruppide nimesid. Tabelid on omavahel seotud GroupId abil. Igal töötajal on oma GUID ja igal grupil oma ID.

Selleks, et programm saaks andmebaasiga suhelda, kasutasin ADO.NET-i ja Microsoft.Data.SqlClient paketti. Tegin ka SqlWorldStorage klassi, mis tegeleb andmete salvestamise ja laadimisega. SQL-käskudes kasutasin parameetreid ning salvestamisel transaktsiooni. Sain teada, et transaktsioon on vajalik selleks, et vea korral ei jääks andmebaasi ainult pool salvestatud andmetest.

Kui andmebaas oli ühendatud, lisasin programmi veel kaks nuppu: Salvesta LocalDB-sse ja Laadi LocalDB-st. Nüüd saan töötajaid salvestada otse andmebaasi, ilma et peaksin ise mingit faili valima.

Kõige põnevam oli muidugi kontrollida, kas see kõik päriselt töötab. Lisasin programmi töötaja Mari, kelle tüüp oli Receptionist, ja vajutasin Salvesta LocalDB-sse. Seejärel kustutasin Mari programmist ära ja vajutasin Laadi LocalDB-st. Mari tuli tagasi! Mul oli päris hea meel, sest see tähendas, et andmed olid tõesti andmebaasi salvestatud.

Seejärel avasin Visual Studios SQL Server Object Exploreri ja vaatasin oma tabeleid. Workers tabelis oli Mari olemas ning Groups tabelis oli tema grupp Receptionist. Nägin ka seda, et tabelid olid GroupId kaudu omavahel seotud.

Tahtsin proovida, kas saan ka töötaja gruppi muuta. Valisin Mari ja kirjutasin tema grupi nimeks Night Shift. Salvestasin andmed uuesti LocalDB-sse ja vaatasin andmebaasi. Groups tabelis oligi nüüd Night Shift ning Workers tabelis oli Mari GroupId väärtus 2. Sama ID oli ka Night Shift grupil. Nii sain aru, et gruppide salvestamine ja tabelitevaheline seos töötavad.

Lõpuks tegin veel ühe väikese katse. Muutsin Mari grupi programmis ajutiselt Test-iks, aga seekord ei salvestanud midagi. Vajutasin hoopis Laadi LocalDB-st ja Mari grupiks tuli jälle Night Shift. See oli täpselt see, mida ootasin, sest andmebaasis oli alles varem salvestatud grupp.

Selle töö käigus õppisin päris palju uusi asju. Sain teada, kuidas töötavad JSON- ja XML-failid, milleks kasutatakse DTO-klasse ja GUID-e ning kuidas programm saab SQL-andmebaasiga ühendust võtta. Samuti õppisin, mis on primaarvõti, välisvõti, SQL-parameetrid ja transaktsioon. Kõige rohkem aega võttis mul ilmselt andmebaasi seadistamine ja selle ühendamine programmiga.

Alguses tundus Vol.4 mulle päris raske, sest uusi asju oli korraga väga palju. Vahepeal ei saanud ma üldse aru, mida järgmisena tegema pean. Aga kui hakkasin kõike väikeste sammudena tegema, läks asi palju selgemaks. Kõige rohkem meeldis mulle see hetk, kui kustutasin Mari ära ja sain ta andmebaasist tagasi laadida. Nüüd on minu hull hotell juba päris korralik programm, sest lisaks töötajate haldamisele ja paranormaalsetele arvutustele oskab see ka oma maailma meelde jätta.


FUNKTSIOONID

Rakendus sisaldab praegu järgmisi võimalusi:

- Hotelli töötajate lisamine ja eemaldamine.
- Kolm töötajatüüpi: administraator, koristaja ja kokk.
- Iga töötaja jaoks erinevad ülesanded ja eriomadused.
- Energiasüsteem, mis muutub töötajate tegevuse käigus.
- Töötajate omavahelised reaktsioonid.
- Töötajate otsimine ja sortimine nime järgi.
- Töötajate filtreerimine tüübi järgi.
- Töötajate rühmitamine atribuudi `Grupp` kaudu.
- Tegevuslogi, mis näitab, mis hotellis toimub.
- Järjestikused ja paralleelsed paranormaalsed arvutused.
- Käimasolevate arvutuste tühistamine.
- Hotelliandmete salvestamine ja laadimine JSON-vormingus.
- Hotelliandmete salvestamine ja laadimine SQL Server LocalDB abil.

TEHNOLOOGIAD

Projekt kasutab järgmisi tehnoloogiaid:

- C# – rakenduse loogika.
- WPF – töölauarakenduse kasutajaliides.
- .NET – rakendusplatvorm.
- Entity Framework Core – andmebaasiga suhtlemine ja objekt-relatsiooniline kaardistamine.
- SQL Server LocalDB – kohalike andmete salvestamine.
- Microsoft.Extensions.DependencyInjection – sõltuvuste süstimine.
- System.Text.Json – JSON-i serialiseerimine ja deserialiseerimine.
- INotifyPropertyChanged – kasutajaliidese uuendamine andmete muutumisel.
- ICommand – nuppude ühendamine rakenduse loogikaga.
- async/await – asünkroonsed toimingud.

PROJEKTI STRUKTUUR

Lahendus koosneb kahest põhiprojektist.

BADHOTEL.CORE

Sisaldab rakenduse põhiloogikat:

- Hotelli töötajate klassid.
- Töötajate liidesed ja võimed.
- Hoidlate klassid.
- Töötajate teisendamise klassid ja DTO-d.
- Arvutusteenused.
- Andmesalvestuse liidesed.
- JSON-salvestuse teostus.
- Entity Framework Core’i andmete püsisalvestuse klassid.

BADHOTEL.WPF

Sisaldab töölauarakendust:

- MainWindow ja selle XAML-liides.
- MainViewModel.
- RelayCommand.
- Rakenduse käivitamine ja sõltuvuste süstimise seadistus.

ANDMETE SALVESTAMINE

Rakendus toetab hotelliandmete salvestamiseks kaht võimalust.

JSON-SALVESTUS

JSON-i teostus kasutab hotelliandmete salvestamiseks ja laadimiseks `System.Text.Json`-i.

Andmed salvestatakse faili `maailm.json`.

JSON-fail sisaldab töötajate andmeid, sealhulgas:

- Töötaja ID.
- Töötaja tüüp.
- Nimi.
- Energia.
- Rühm.

Rakendus saab salvestada hotelli praeguse oleku faili ja selle hiljem taastada.

SQL SERVER LOCALDB

Andmebaasi teostus kasutab Entity Framework Core’i koos SQL Server LocalDB-ga.

Andmebaasi nimi on `BadHotelEfDb`.

See sisaldab kaht põhitabelit:

- `Workers` – sisaldab töötajate andmeid.
- `Groups` – sisaldab töötajate rühmi.

Töötajate ja rühmade vaheline seos on seadistatud võõrvõtme abil.

Andmebaasis on ka piirang, mis takistab töötajate energial langeda alla 0 või tõusta üle 100.

ENTITY FRAMEWORK CORE’I PÄRILUS

Projekt kasutab TPH-pärilusstrateegiat (Table-per-Hierarchy).

See tähendab, et eri tüüpi töötajaid hoitakse samas tabelis `Workers`.

Baasklass on `WorkerEntity`, millel on kolm tuletatud klassi:

- `ReceptionistEntity`
- `CleanerEntity`
- `CookEntity`

Entity Framework Core kasutab töötaja tüübi tuvastamiseks eristavat veergu `Type`.

Nii jääb andmebaasi struktuur suhteliselt lihtsaks, kuid rakendus saab siiski töötada eri tüüpi töötajatega.

SÕLTUVUSTE SÜSTIMINE

Sõltuvuste süstimine on seadistatud failis `App.xaml.cs`, kasutades `Microsoft.Extensions.DependencyInjection`-i.

Rakendus registreerib järgmised teenused:

- `IWorldStorage` – Entity Framework Core’i salvestusliides.
- `EfWorldStorage` – andmebaasi salvestus.
- `JsonWorldStorage` – JSON-faili salvestus.
- `MainViewModel` – rakenduse esitusloogika.
- `MainWindow` – rakenduse peaaken.

Mõlemad salvestuslahendused on saadaval sõltuvuste süstimise konteineri kaudu.

See muudab rakenduse hooldamise lihtsamaks ja vähendab selle komponentide otseseid sõltuvusi üksteisest.

ANDMETE SALVESTAMINE JA LAADIMINE

Peaaknas on neli eraldi nuppu:

| Nupp                 | Eesmärk                              |
|----------------------|--------------------------------------|
| Salvesta maailm      | Salvestab hotelliandmed JSON-faili.  |
| Laadi maailm         | Laadib hotelliandmed JSON-failist.   |
| Salvesta LocalDB-sse | Salvestab hotelliandmed LocalDB-sse. |
| Laadi LocalDB-st     | Laadib hotelliandmed LocalDB-st.     |

JSON-i ja andmebaasi salvestuslahendused kasutavad sama `IWorldStorage`-liidest.

See võimaldab rakendusel töötada eri salvestussüsteemidega, ilma et põhiandmemudelit peaks muutma.

ANDMEBAASI MIGRATSIOONID

Andmebaasi muudatuste haldamiseks kasutatakse Entity Framework Core’i migratsioone.

Praegu sisaldab projekt järgmisi migratsioone:

- `InitialEfCore`
- `AddWorkerTph`

Migratsioonikäske saab käivitada Visual Studio Package Manager Console’is.

Migratsiooni loomine:

    Add-Migration MigrationName -Project BadHotel.Core -StartupProject BadHotel.Wpf -Context HotelDbContext

Migratsioonide rakendamine:

    Update-Database -Project BadHotel.Core -StartupProject BadHotel.Wpf -Context HotelDbContext

Olemasolevate migratsioonide loetlemine:

    Get-Migration -Context HotelDbContext

RAKENDUSE KÄIVITAMINE

Projektiga töötamiseks:

1. Ava Visual Studios `BadHotelNew.sln`.
2. Veendu, et vajalikud NuGeti paketid oleks taastatud.
3. Kui vajad andmebaasi funktsioone, veendu, et SQL Server LocalDB oleks saadaval.
4. Määra käivitusprojektiks `BadHotel.Wpf`.
5. Koosta lahendus.
6. Käivita rakendus.

Andmebaasiühendus on seadistatud failis `App.xaml.cs`.

PRAEGUNE ARENGUETAPP

Projektile on lisatud JSON-salvestus, Entity Framework Core, LocalDB tugi, TPH-pärilus ja sõltuvuste süstimine.

Lahendus kompileerub praegu edukalt.

Järgmine samm on salvestustoimingute käivitusaegne testimine ning kontrollimine, et töötajate andmed säilivad salvestamisel ja laadimisel õigesti.

LÕPUMÄRKUS

BadHotel on olnud praktiline viis C#, WPF-i, objektorienteeritud programmeerimise, andmebaasiseoste ja rakenduse arhitektuuri tundmaõppimiseks.

Projekt ühendab mitu mõistet ühes rakenduses ning loob aluse edasisteks arendustöödeks.




