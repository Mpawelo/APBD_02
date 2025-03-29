using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        try
        {
            var gasContainer = new GasContainer(
                ownWeight: 3000,
                height: 350,
                depth: 600,
                maxCapacity: 12000,
                pressure: 5.2);
            gasContainer.Load(8000);

            var refrigeratedContainer = new RefrigeratedContainer(
                ownWeight: 2800,
                height: 320,
                depth: 550,
                maxCapacity: 9000,
                productType: "Fish",
                temperature: 3);
            refrigeratedContainer.Load(6000);

            var liquidContainer = new LiquidContainer(
                ownWeight: 2500,
                height: 300,
                depth: 500,
                maxCapacity: 10000,
                isHazardous: false);
            liquidContainer.Load(8500);

            var ship1 = new ContainerShip(maxSpeed: 25, maxContainerCount: 10, maxTotalWeight: 100);
            var ship2 = new ContainerShip(maxSpeed: 20, maxContainerCount: 5, maxTotalWeight: 50);

            ship1.AddContainer(gasContainer);
            ship1.AddContainer(refrigeratedContainer);

            ship1.AddContainers(new List<Container> { liquidContainer });

            ship1.PrintShipInfo();

            ship1.PrintContainerInfo(gasContainer.SerialNumber);

            ship1.UnloadContainer(refrigeratedContainer.SerialNumber);
            Console.WriteLine($"Po rozładowaniu, ładunek kontenera {refrigeratedContainer.SerialNumber}: {refrigeratedContainer.LoadMass} kg");

            var newGasContainer = new GasContainer(
                ownWeight: 3100,
                height: 350,
                depth: 600,
                maxCapacity: 12500,
                pressure: 5.5);
            newGasContainer.Load(9000);
            ship1.ReplaceContainer(refrigeratedContainer.SerialNumber, newGasContainer);
            Console.WriteLine("Po zastąpieniu kontenera:");
            ship1.PrintShipInfo();

            ship1.RemoveContainer(gasContainer.SerialNumber);
            Console.WriteLine("Po usunięciu jednego kontenera:");
            ship1.PrintShipInfo();

            ship1.TransferContainer(newGasContainer.SerialNumber, ship2);
            Console.WriteLine("Po przeniesieniu kontenera do drugiego statku:");
            Console.WriteLine("Statek 1:");
            ship1.PrintShipInfo();
            Console.WriteLine("Statek 2:");
            ship2.PrintShipInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}