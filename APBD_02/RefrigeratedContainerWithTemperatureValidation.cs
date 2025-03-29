public class RefrigeratedContainer : Container, IHazardNotifier
{
    private static readonly Dictionary<string, double> ProductTemperatureRequirements = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };

    public string ProductType { get; }
    public double Temperature { get; private set; }

    public RefrigeratedContainer(double ownWeight, int height, int depth, double maxCapacity, string productType, double temperature)
        : base(ownWeight, height, depth, maxCapacity, "C")
    {
        if (!ProductTemperatureRequirements.TryGetValue(productType, out double requiredTemperature))
        {
            throw new ArgumentException($"Nieznany typ produktu: {productType}.", nameof(productType));
        }

        if (temperature < requiredTemperature)
        {
            throw new ArgumentException($"Temperatura kontenera ({temperature}°C) nie może być niższa niż wymagana temperatura dla {productType} ({requiredTemperature}°C).");
        }

        ProductType = productType;
        Temperature = temperature;
    }

    public void NotifyDanger(string message)
    {
        Console.WriteLine($"[ALERT] Kontener {SerialNumber}: {message}");
    }
}