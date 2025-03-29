public abstract class Container
{
    private static int _serialNumberCounter = 1;
    
    public string SerialNumber { get; }
    public double LoadMass { get; protected set; }
    public int Height { get; }
    public int Depth { get; }
    public double OwnWeight { get; }
    public double MaxCapacity { get; }

    protected Container(double ownWeight, int height, int depth, double maxCapacity, string typeCode)
    {
        OwnWeight = ownWeight;
        Height = height;
        Depth = depth;
        MaxCapacity = maxCapacity;

        SerialNumber = $"KON-{typeCode}-{_serialNumberCounter++}";
    }

    public virtual void Load(double weight)
    {
        if (LoadMass + weight > MaxCapacity)
        {
            throw new OverfillException($"Próba załadowania {weight} kg przekracza maksymalną pojemność kontenera ({MaxCapacity} kg).");
        }

        LoadMass += weight;
    }

    public virtual void Unload()
    {
        LoadMass = 0;
    }
}