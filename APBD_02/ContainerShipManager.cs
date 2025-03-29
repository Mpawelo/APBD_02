public class ContainerShip
{
    public double MaxSpeed { get; }
    public int MaxContainerCount { get; }
    public double MaxTotalWeight { get; }
    
    private readonly List<Container> _containers = new List<Container>();
    public IReadOnlyList<Container> Containers => _containers.AsReadOnly();

    public ContainerShip(double maxSpeed, int maxContainerCount, double maxTotalWeight)
    {
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxTotalWeight = maxTotalWeight;
    }

    private double TotalWeightKg()
    {
        return _containers.Sum(c => c.OwnWeight + c.LoadMass);
    }

    public void AddContainer(Container container)
    {
        if (_containers.Count >= MaxContainerCount)
            throw new Exception("Osiągnięto maksymalną liczbę kontenerów, jakie statek może przewozić.");

        double newTotalWeightTons = (TotalWeightKg() + container.OwnWeight + container.LoadMass) / 1000.0;
        if (newTotalWeightTons > MaxTotalWeight)
            throw new Exception("Przekroczony został maksymalny dopuszczalny łączny ciężar kontenerów (w tonach).");

        _containers.Add(container);
    }

    public void AddContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            AddContainer(container);
        }
    }

    public void RemoveContainer(string serialNumber)
    {
        var container = _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
            throw new Exception($"Kontener o numerze {serialNumber} nie został znaleziony na statku.");
        _containers.Remove(container);
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
            throw new Exception($"Kontener o numerze {serialNumber} nie został znaleziony na statku.");
        container.Unload();
    }

    public void ReplaceContainer(string serialNumber, Container newContainer)
    {
        int index = _containers.FindIndex(c => c.SerialNumber == serialNumber);
        if (index == -1)
            throw new Exception($"Kontener o numerze {serialNumber} nie został znaleziony na statku.");

        Container oldContainer = _containers[index];
        _containers.RemoveAt(index);
        try
        {
            AddContainer(newContainer);
        }
        catch (Exception ex)
        {
            _containers.Insert(index, oldContainer);
            throw new Exception("Zastąpienie kontenera nie powiodło się: " + ex.Message);
        }
    }

    public void TransferContainer(string serialNumber, ContainerShip targetShip)
    {
        var container = _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
            throw new Exception($"Kontener o numerze {serialNumber} nie został znaleziony na statku źródłowym.");
        targetShip.AddContainer(container);
        _containers.Remove(container);
    }

    public void PrintContainerInfo(string serialNumber)
    {
        var container = _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Kontener o numerze {serialNumber} nie został znaleziony na statku.");
            return;
        }
        Console.WriteLine("Informacje o kontenerze:");
        Console.WriteLine($"Numer seryjny: {container.SerialNumber}");
        Console.WriteLine($"Waga własna: {container.OwnWeight} kg");
        Console.WriteLine($"Aktualny ładunek: {container.LoadMass} kg");
        Console.WriteLine($"Maksymalna ładowność: {container.MaxCapacity} kg");
    }

    public void PrintShipInfo()
    {
        Console.WriteLine("=== Informacje o statku ===");
        Console.WriteLine($"Maksymalna prędkość: {MaxSpeed} węzłów");
        Console.WriteLine($"Maksymalna liczba kontenerów: {MaxContainerCount}");
        Console.WriteLine($"Maksymalna łączna waga kontenerów: {MaxTotalWeight} ton");
        Console.WriteLine("Lista kontenerów:");
        foreach (var container in _containers)
        {
            Console.WriteLine($"- {container.SerialNumber}: Waga własna {container.OwnWeight} kg; Ładunek {container.LoadMass} kg");
        }
        Console.WriteLine("===========================");
    }
}