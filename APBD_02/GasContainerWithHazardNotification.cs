public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; }

    public GasContainer(double ownWeight, int height, int depth, double maxCapacity, double pressure)
        : base(ownWeight, height, depth, maxCapacity, "G")
    {
        Pressure = pressure;
    }

    public void NotifyDanger(string message)
    {
        Console.WriteLine($"[ALERT] Kontener {SerialNumber}: {message}");
    }

    public override void Load(double weight)
    {
        if (LoadMass + weight > MaxCapacity)
        {
            NotifyDanger($"Próba załadowania {weight} kg (łączna masa: {LoadMass + weight} kg) przekracza maksymalną pojemność kontenera ({MaxCapacity} kg).");
            throw new OverfillException($"Nie można załadować {weight} kg, dozwolona masa kontenera to {MaxCapacity} kg.");
        }
        
        base.Load(weight);
    }

    public override void Unload()
    {
        double remainingMass = LoadMass * 0.05;
        LoadMass = remainingMass;
    }
}