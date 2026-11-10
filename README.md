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











Vol.4 käigus lisasin oma BadHotelNew programmi võimaluse salvestada ja laadida kogu hotelli maailma. Nüüd ei kao töötajate andmed pärast programmi sulgemist, kui olen need eelnevalt salvestanud. Salvestatakse iga töötaja nimi, tüüp, energia, grupp ja unikaalne GUID. See on väga mugav, sest saan hiljem jätkata samade töötajatega.

Kõigepealt lisasin JSON- ja XML-failide kasutamise. Kui vajutan nuppu Salvesta maailm, saan valida, kas salvestada andmed JSON- või XML-faili. Nupuga Laadi maailm saan salvestatud töötajad tagasi laadida. Selle jaoks kasutasin klasse JsonWorldStorage ja XmlWorldStorage. Mõlemad kasutavad IWorldStorage liidest. Andmete teisendamiseks lõin WorkerDto, WorldDto ja WorldMapper klassid. WorldStorageService aitab valida õige salvestusviisi.

Oluline on ka see, et programm kontrollib laaditavaid andmeid. Näiteks proovisin laadida JSON-faili, kus töötaja GUID oli vigane. Programm näitas veateadet, aga olemasolev töötaja jäi alles. Nii ei kaota ma vigase faili tõttu oma hotelli maailma.

Seejärel lisasin SQL Server Express LocalDB andmebaasi. Minu andmebaasi nimi on BadHotelDb. Andmebaasis on kaks tabelit: Workers ja Groups. Workers tabelis on töötaja GUID, tüüp, nimi, energia ja GroupId. Groups tabelis on grupi ID ja nimi. Tabelid on omavahel seotud välisvõtme abil, sest Workers tabeli GroupId viitab Groups tabeli Id väärtusele.

Andmebaasiga töötamiseks kasutasin ADO.NET tehnoloogiat ja Microsoft.Data.SqlClient paketti. Lõin SqlWorldStorage klassi, mis võimaldab töötajaid andmebaasi salvestada ja sealt laadida. SQL-käskudes kasutan parameetreid ning salvestamisel transaktsiooni. Kui salvestamisel tekib viga, saab transaktsiooni tagasi võtta.

Programmi lisasin kaks uut nuppu: Salvesta LocalDB-sse ja Laadi LocalDB-st. Esimene salvestab töötajad andmebaasi ning teine taastab need. LocalDB kasutamisel ei pea eraldi faili valima.

Kontrollisin ka andmebaasi tööd. Lisasin programmi töötaja Mari ja salvestasin ta LocalDB-sse. Seejärel eemaldasin Mari programmist ja laadisin maailma uuesti andmebaasist. Mari ilmus tagasi! Visual Studio SQL Server Object Explorer aknas nägin, et Mari oli salvestatud Workers tabelisse ja tema grupp Groups tabelisse.

Lisaks muutsin Mari grupi nime Receptionist asemel Night Shift. Pärast salvestamist nägin andmebaasis uut gruppi Night Shift. Workers tabelis oli Mari GroupId väärtus 2 ja Groups tabelis oli sama ID-ga grupp Night Shift. See näitas, et tabelitevaheline seos töötab õigesti.

Lõpuks proovisin ka grupi taastamist. Muutsin programmis Mari grupi ajutiselt Test-iks, aga ei salvestanud seda. Kui vajutasin Laadi LocalDB-st, muutus grupp tagasi Night Shift-iks. See tõestas, et programm taastab andmebaasist ka töötaja grupi.

Vol.4 käigus õppisin kasutama JSON- ja XML-faile, DTO-klasse, GUID-identifikaatoreid, liideseid, sõltuvuste edastamist konstruktori kaudu, ADO.NET-i, SQL-andmebaasi, primaarvõtmeid, välisvõtmeid, parameetritega SQL-käske ja transaktsioone. Alguses tundus andmebaasi ühendamine väga keeruline, aga lõpuks sain kõik tööle. Nüüd oskab minu hull hotell mitte ainult töötajatega tegutseda ja paranormaalseid arvutusi teha, vaid ka kogu oma maailma salvestada ja taastada!
