public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; }

    public LiquidContainer(double ownWeight, int height, int depth, double maxCapacity, bool isHazardous)
        : base(ownWeight, height, depth, maxCapacity, "L")
    {
        IsHazardous = isHazardous;
    }

    public void NotifyDanger(string message)
    {
        Console.WriteLine($"[ALERT] Kontener {SerialNumber}: {message}");
    }

    public override void Load(double weight)
    {
        double allowedCapacity = IsHazardous ? MaxCapacity * 0.5 : MaxCapacity * 0.9;

        if (LoadMass + weight > allowedCapacity)
        {
            NotifyDanger($"Próba załadowania {weight} kg (łączna masa: {LoadMass + weight} kg) przekracza dozwoloną pojemność ({allowedCapacity} kg) dla ładunku {(IsHazardous ? "niebezpiecznego" : "zwykłego")}.");
            throw new OverfillException($"Nie można załadować {weight} kg! Dopuszczalna masa ładunku tego kontenera to {allowedCapacity} kg.");
        }

        base.Load(weight);
    }
}