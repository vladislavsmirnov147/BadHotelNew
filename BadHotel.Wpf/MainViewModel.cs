
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BadHotel.Core;
using BadHotel.Core.Persistence;

namespace BadHotel.Wpf;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IWorldStorage _worldStorage;
    private readonly JsonWorldStorage _fileStorage;

    private readonly Repository<Receptionist> _receptionists = new();
    private readonly Repository<Cleaner> _cleaners = new();
    private readonly Repository<Cook> _cooks = new();

    private readonly RepositoryReactions<Receptionist> _receptionistReactions;
    private readonly RepositoryReactions<Cleaner> _cleanerReactions;
    private readonly RepositoryReactions<Cook> _cookReactions;

    private readonly CalculationService _arvutusteenus = new();

    private CancellationTokenSource? _tuhistamiseAllikas;

    private bool _salvestamineKaib;

    private string _newWorkerName = string.Empty;
    private string? _selectedWorkerType;
    private string _selectedRepository = "All";
    private HotelWorker? _selectedWorker;
    private string _searchText = string.Empty;
    private bool _sortByName;

    private bool _neetudTubaValitud = true;
    private bool _kummituslikLiftValitud = true;
    private bool _neetudPeegelValitud = true;
    private bool _veaTest;
    private string _valitudReziim = "Järjestikune";
    private int _arvutusteMaht = 10000000;
    private bool _arvutusKaib;
    private string _koguAeg = "Arvutusi pole veel tehtud.";

    public ObservableCollection<HotelWorker> Workers { get; } = new();
    public ObservableCollection<HotelWorker> DisplayedWorkers { get; } = new();
    public ObservableCollection<string> ActivityLog { get; } = new();
    public ObservableCollection<string> WorkerSummary { get; } = new();

    public ObservableCollection<string> WorkerTypes { get; } = new()
    {
        "Receptionist",
        "Cleaner",
        "Cook"
    };

    public ObservableCollection<string> RepositoryTypes { get; } = new()
    {
        "All",
        "Receptionist",
        "Cleaner",
        "Cook"
    };

    public ObservableCollection<string> Arvutustulemused { get; } = new();

    public ObservableCollection<string> Arvutusreziimid { get; } = new()
    {
        "Järjestikune",
        "Paralleelne"
    };

    public string NewWorkerName
    {
        get => _newWorkerName;
        set
        {
            _newWorkerName = value;
            OnPropertyChanged();
            AddCommand.RaiseCanExecuteChanged();
        }
    }

    public string? SelectedWorkerType
    {
        get => _selectedWorkerType;
        set
        {
            _selectedWorkerType = value;
            OnPropertyChanged();
            AddCommand.RaiseCanExecuteChanged();
        }
    }

    public string SelectedRepository
    {
        get => _selectedRepository;
        set
        {
            _selectedRepository = value;
            OnPropertyChanged();
            SelectedWorker = null;
            UpdateDisplayedWorkers();
        }
    }

    public HotelWorker? SelectedWorker
    {
        get => _selectedWorker;
        set
        {
            _selectedWorker = value;
            OnPropertyChanged();
            WorkCommand.RaiseCanExecuteChanged();
            SpecialAbilityCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            CrazyActionCommand.RaiseCanExecuteChanged();
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            UpdateDisplayedWorkers();
        }
    }

    public bool SortByName
    {
        get => _sortByName;
        set
        {
            _sortByName = value;
            OnPropertyChanged();
            UpdateDisplayedWorkers();
        }
    }

    public bool NeetudTubaValitud
    {
        get => _neetudTubaValitud;
        set
        {
            _neetudTubaValitud = value;
            OnPropertyChanged();
            UuendaArvutuskaske();
        }
    }

    public bool KummituslikLiftValitud
    {
        get => _kummituslikLiftValitud;
        set
        {
            _kummituslikLiftValitud = value;
            OnPropertyChanged();
            UuendaArvutuskaske();
        }
    }

    public bool NeetudPeegelValitud
    {
        get => _neetudPeegelValitud;
        set
        {
            _neetudPeegelValitud = value;
            OnPropertyChanged();
            UuendaArvutuskaske();
        }
    }

    public bool VeaTest
    {
        get => _veaTest;
        set
        {
            _veaTest = value;
            OnPropertyChanged();
        }
    }

    public string ValitudReziim
    {
        get => _valitudReziim;
        set
        {
            _valitudReziim = value;
            OnPropertyChanged();
        }
    }

    public int ArvutusteMaht
    {
        get => _arvutusteMaht;
        set
        {
            _arvutusteMaht = value;
            OnPropertyChanged();
            UuendaArvutuskaske();
        }
    }

    public bool ArvutusKaib
    {
        get => _arvutusKaib;
        private set
        {
            _arvutusKaib = value;
            OnPropertyChanged();
            UuendaArvutuskaske();
        }
    }

    public string KoguAeg
    {
        get => _koguAeg;
        private set
        {
            _koguAeg = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand AddCommand { get; }
    public RelayCommand WorkCommand { get; }
    public RelayCommand SpecialAbilityCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand CrazyActionCommand { get; }

    public RelayCommand SaveWorldCommand { get; }
    public RelayCommand LoadWorldCommand { get; }
    public RelayCommand SaveDatabaseCommand { get; }
    public RelayCommand LoadDatabaseCommand { get; }

    public RelayCommand AlustaArvutustCommand { get; }
    public RelayCommand TuhistaArvutusCommand { get; }

    public MainViewModel(
        IWorldStorage worldStorage,
        JsonWorldStorage fileStorage)
    {
        _worldStorage = worldStorage
            ?? throw new ArgumentNullException(nameof(worldStorage));

        _fileStorage = fileStorage
            ?? throw new ArgumentNullException(nameof(fileStorage));

        _receptionistReactions =
            new RepositoryReactions<Receptionist>(_receptionists);

        _cleanerReactions =
            new RepositoryReactions<Cleaner>(_cleaners);

        _cookReactions =
            new RepositoryReactions<Cook>(_cooks);

        _receptionistReactions.ReactionCreated += AddReactionToLog;
        _cleanerReactions.ReactionCreated += AddReactionToLog;
        _cookReactions.ReactionCreated += AddReactionToLog;

        AddCommand = new RelayCommand(AddWorker, CanAddWorker);

        WorkCommand = new RelayCommand(
            DoWork,
            () => SelectedWorker is not null);

        SpecialAbilityCommand = new RelayCommand(
            DoSpecialAbility,
            () => SelectedWorker is not null);

        RemoveCommand = new RelayCommand(
            RemoveWorker,
            () => SelectedWorker is not null);

        CrazyActionCommand = new RelayCommand(
            DoCrazyAction,
            () => SelectedWorker is not null);

        SaveWorldCommand = new RelayCommand(
            () => _ = SaveAsync(_fileStorage, "maailm.json", "Faili"),
            () => !_salvestamineKaib);

        LoadWorldCommand = new RelayCommand(
            () => _ = LoadAsync(_fileStorage, "maailm.json", "Failist"),
            () => !_salvestamineKaib);

        SaveDatabaseCommand = new RelayCommand(
            () => _ = SaveAsync(_worldStorage, string.Empty, "LocalDB-sse"),
            () => !_salvestamineKaib);

        LoadDatabaseCommand = new RelayCommand(
            () => _ = LoadAsync(_worldStorage, string.Empty, "LocalDB-st"),
            () => !_salvestamineKaib);

        AlustaArvutustCommand = new RelayCommand(
            () => _ = AlustaArvutustAsync(),
            SaabArvutustAlustada);

        TuhistaArvutusCommand = new RelayCommand(
            TuhistaArvutus,
            () => ArvutusKaib);

        UpdateDisplayedWorkers();
        UpdateWorkerSummary();
    }

    private void UuendaSalvestamiskaske()
    {
        SaveWorldCommand.RaiseCanExecuteChanged();
        LoadWorldCommand.RaiseCanExecuteChanged();
        SaveDatabaseCommand.RaiseCanExecuteChanged();
        LoadDatabaseCommand.RaiseCanExecuteChanged();
    }

    private WorldDto CreateWorldDto()
    {
        return new WorldDto
        {
            Workers = Workers.Select(worker =>
                WorkerMapper.ToDto(worker, worker.Group)).ToList()
        };
    }

    private async Task SaveAsync(
        IWorldStorage storage,
        string filePath,
        string koht)
    {
        if (_salvestamineKaib)
            return;

        _salvestamineKaib = true;
        UuendaSalvestamiskaske();

        try
        {
            WorldDto world = CreateWorldDto();

            await storage.SaveAsync(filePath, world);

            ActivityLog.Add(
                $"Maailm salvestati edukalt. Asukoht: {koht}. " +
                $"Töötajaid: {world.Workers.Count}.");
        }
        catch (Exception ex)
        {
            ActivityLog.Add($"Salvestamise viga: {ex.Message}");
        }
        finally
        {
            _salvestamineKaib = false;
            UuendaSalvestamiskaske();
        }
    }

    private async Task LoadAsync(
        IWorldStorage storage,
        string filePath,
        string koht)
    {
        if (_salvestamineKaib)
            return;

        _salvestamineKaib = true;
        UuendaSalvestamiskaske();

        try
        {
            WorldDto world = await storage.LoadAsync(filePath);

            List<HotelWorker> loadedWorkers = world.Workers
                .Select(WorkerMapper.FromDto)
                .ToList();

            SelectedWorker = null;

            foreach (HotelWorker worker in Workers.ToList())
                RemoveFromRepository(worker);

            Workers.Clear();

            foreach (HotelWorker worker in loadedWorkers)
            {
                AddToRepository(worker);
                Workers.Add(worker);
            }

            UpdateDisplayedWorkers();
            UpdateWorkerSummary();

            ActivityLog.Add(
                $"Maailm laaditi edukalt. Allikas: {koht}. " +
                $"Töötajaid: {Workers.Count}.");
        }
        catch (Exception ex)
        {
            ActivityLog.Add($"Laadimise viga: {ex.Message}");
        }
        finally
        {
            _salvestamineKaib = false;
            UuendaSalvestamiskaske();
        }
    }

    private void AddToRepository(HotelWorker worker)
    {
        switch (worker)
        {
            case Receptionist receptionist:
                _receptionists.Add(receptionist);
                break;

            case Cleaner cleaner:
                _cleaners.Add(cleaner);
                break;

            case Cook cook:
                _cooks.Add(cook);
                break;
        }
    }

    private void RemoveFromRepository(HotelWorker worker)
    {
        switch (worker)
        {
            case Receptionist receptionist:
                _receptionists.Remove(receptionist);
                break;

            case Cleaner cleaner:
                _cleaners.Remove(cleaner);
                break;

            case Cook cook:
                _cooks.Remove(cook);
                break;
        }
    }

    private void UuendaArvutuskaske()
    {
        AlustaArvutustCommand.RaiseCanExecuteChanged();
        TuhistaArvutusCommand.RaiseCanExecuteChanged();
    }

    private bool SaabArvutustAlustada()
    {
        return !ArvutusKaib
            && ArvutusteMaht > 0
            && (NeetudTubaValitud
                || KummituslikLiftValitud
                || NeetudPeegelValitud);
    }

    private List<CalculationSnapshot> LooArvutusteAndmed()
    {
        List<CalculationSnapshot> andmed = new();

        if (NeetudTubaValitud)
        {
            andmed.Add(new CalculationSnapshot(
                "Tuba 13",
                "Neetud tuba",
                ArvutusteMaht,
                7,
                13));
        }

        if (KummituslikLiftValitud)
        {
            andmed.Add(new CalculationSnapshot(
                "Hotelli lift",
                "Kummituslik lift",
                ArvutusteMaht,
                8,
                VeaTest ? 1 : 12));
        }

        if (NeetudPeegelValitud)
        {
            andmed.Add(new CalculationSnapshot(
                "Vana peegel",
                "Neetud peegel",
                ArvutusteMaht,
                9,
                25));
        }

        return andmed;
    }

    private async Task AlustaArvutustAsync()
    {
        if (!SaabArvutustAlustada())
            return;

        List<CalculationSnapshot> andmed = LooArvutusteAndmed();

        _tuhistamiseAllikas = new CancellationTokenSource();

        CancellationToken tuhistamiseToken =
            _tuhistamiseAllikas.Token;

        ArvutusKaib = true;
        Arvutustulemused.Clear();
        KoguAeg = "Arvutamine käib...";

        Stopwatch stopper = Stopwatch.StartNew();

        try
        {
            List<CalculationResult> tulemused;

            if (ValitudReziim == "Paralleelne")
            {
                tulemused = await _arvutusteenus.RunParallelAsync(
                    andmed,
                    tuhistamiseToken);
            }
            else
            {
                tulemused = await _arvutusteenus.RunSequentialAsync(
                    andmed,
                    tuhistamiseToken);
            }

            foreach (CalculationResult tulemus in tulemused)
            {
                Arvutustulemused.Add(
                    $"Objekt: {tulemus.ObjectName}\n" +
                    $"Olek: {tulemus.Status}\n" +
                    $"Tulemus: {tulemus.Result}\n" +
                    $"Kestus: {tulemus.DurationMilliseconds} ms");
            }
        }
        catch (Exception ex)
        {
            Arvutustulemused.Add($"Viga: {ex.Message}");
        }
        finally
        {
            stopper.Stop();

            KoguAeg =
                $"Režiim: {ValitudReziim}. " +
                $"Koguaeg: {stopper.ElapsedMilliseconds} ms";

            ArvutusKaib = false;

            _tuhistamiseAllikas?.Dispose();
            _tuhistamiseAllikas = null;
        }
    }

    private void TuhistaArvutus()
    {
        _tuhistamiseAllikas?.Cancel();
    }

    private void AddReactionToLog(string teade)
    {
        ActivityLog.Add(teade);
    }

    private bool CanAddWorker()
    {
        return !string.IsNullOrWhiteSpace(NewWorkerName)
            && !string.IsNullOrWhiteSpace(SelectedWorkerType);
    }

    private void AddWorker()
    {
        string nimi = NewWorkerName.Trim();

        switch (SelectedWorkerType)
        {
            case "Receptionist":
                Receptionist vastuvotutootaja = new(nimi);
                _receptionists.Add(vastuvotutootaja);
                Workers.Add(vastuvotutootaja);
                ActivityLog.Add(
                    $"{vastuvotutootaja.Name} alustas tööd vastuvõtutöötajana.");
                SelectedWorker = vastuvotutootaja;
                break;

            case "Cleaner":
                Cleaner koristaja = new(nimi);
                _cleaners.Add(koristaja);
                Workers.Add(koristaja);
                ActivityLog.Add(
                    $"{koristaja.Name} alustas tööd koristajana.");
                SelectedWorker = koristaja;
                break;

            case "Cook":
                Cook kokk = new(nimi);
                _cooks.Add(kokk);
                Workers.Add(kokk);
                ActivityLog.Add(
                    $"{kokk.Name} alustas tööd kokana.");
                SelectedWorker = kokk;
                break;
        }

        NewWorkerName = string.Empty;
        SelectedWorkerType = null;

        UpdateDisplayedWorkers();
        UpdateWorkerSummary();
    }

    private void DoWork()
    {
        if (SelectedWorker is null)
            return;

        ActivityLog.Add(SelectedWorker.Work());
    }

    private void DoSpecialAbility()
    {
        if (SelectedWorker is null)
            return;

        string teade;

        if (SelectedWorker is ICook kokk)
            teade = kokk.CookMeal();
        else if (SelectedWorker is IClean koristaja)
            teade = koristaja.CleanRoom();
        else if (SelectedWorker is IServe teenindaja)
            teade = teenindaja.ServeGuest();
        else
            teade = $"{SelectedWorker.Name} ei oma erivõimet.";

        ActivityLog.Add(teade);
    }

    private void RemoveWorker()
    {
        if (SelectedWorker is null)
            return;

        HotelWorker eemaldatudTootaja = SelectedWorker;

        RemoveFromRepository(eemaldatudTootaja);
        Workers.Remove(eemaldatudTootaja);

        ActivityLog.Add(
            $"{eemaldatudTootaja.Name} lahkus hotellist.");

        SelectedWorker = null;

        UpdateDisplayedWorkers();
        UpdateWorkerSummary();
    }

    private void DoCrazyAction()
    {
        if (SelectedWorker is Receptionist vastuvotutootaja)
        {
            ActivityLog.Add(
                _receptionists.PerformCrazyAction(vastuvotutootaja));
        }
        else if (SelectedWorker is Cleaner koristaja)
        {
            ActivityLog.Add(
                _cleaners.PerformCrazyAction(koristaja));
        }
        else if (SelectedWorker is Cook kokk)
        {
            ActivityLog.Add(
                _cooks.PerformCrazyAction(kokk));
        }
    }

    private void UpdateDisplayedWorkers()
    {
        IEnumerable<HotelWorker> tulemus = Workers;

        if (SelectedRepository != "All")
        {
            tulemus = tulemus.Where(
                tootaja => tootaja.GetType().Name == SelectedRepository);
        }

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            tulemus = tulemus.Where(
                tootaja => tootaja.Name.Contains(
                    SearchText,
                    StringComparison.OrdinalIgnoreCase));
        }

        if (SortByName)
            tulemus = tulemus.OrderBy(tootaja => tootaja.Name);

        DisplayedWorkers.Clear();

        foreach (HotelWorker tootaja in tulemus)
            DisplayedWorkers.Add(tootaja);
    }

    private void UpdateWorkerSummary()
    {
        var grupid = Workers
            .GroupBy(tootaja => tootaja.GetType().Name)
            .OrderBy(grupp => grupp.Key);

        WorkerSummary.Clear();

        foreach (var grupp in grupid)
            WorkerSummary.Add($"{grupp.Key}: {grupp.Count()}");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}
