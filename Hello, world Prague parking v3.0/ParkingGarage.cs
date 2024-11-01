using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello__world_Prague_parking_v3._0
{
    public class ParkingGarage
    {
        private List<List<ParkingSpot>> parkingSpots;
        int n;

        public ParkingGarage(List<List<ParkingSpot>> spots)
        {
            n = LoadTotalSpotsFromConfig();
            parkingSpots = spots ?? new List<List<ParkingSpot>>(new List<ParkingSpot>[n]);
            for (int i = 0; i < parkingSpots.Count; i++)
            {
                if (parkingSpots[i] == null)
                {
                    parkingSpots[i] = new List<ParkingSpot>();
                }
            }
        }

        private int LoadTotalSpotsFromConfig()
        {
            string config = File.ReadAllText("Konfigfil.txt");
            var lines = config.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.StartsWith("TotalParkingSpots"))
                {
                    string[] parts = line.Split(':');
                    return int.Parse(parts[1].Trim());
                }
            }

            return 100; // Default if config value not found
        }
        public static int LoadCarSizeFromConfig()
        {
            string config = File.ReadAllText("Konfigfil.txt");
            var lines = config.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.StartsWith("Car"))
                {
                    string[] parts = line.Split(':');
                    return int.Parse(parts[1].Trim());
                }
            }

            return 4; // Default if config value not found
        }

        public static int LoadMCSizeFromConfig()
        {
            string config = File.ReadAllText("Konfigfil.txt");
            var lines = config.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.StartsWith("Motorcycle"))
                {
                    string[] parts = line.Split(':');
                    return int.Parse(parts[1].Trim());
                }
            }

            return 2; // Default if config value not found
        }

        public static int LoadSpotSizeFromConfig()
        {
            string config = File.ReadAllText("Konfigfil.txt");
            var lines = config.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.StartsWith("ParkingSpotSize"))
                {
                    string[] parts = line.Split(':');
                    return int.Parse(parts[1].Trim());
                }
            }

            return 2; // Default if config value not found
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ViewParking()
        {
            Console.WriteLine();
            AnsiConsole.Markup($"[blue]{"Car is blue"} [/]");
            Console.WriteLine();
            AnsiConsole.Markup($"[red]{"Motorcycle is red"} [/]");
            Console.WriteLine("\n");

            for (int i = 1; i < parkingSpots.Count; i++)
            {
                if (parkingSpots[i].Count == 0)
                {
                    Console.Write($"|Spot {i}: Empty");
                    if (i % 5 == 0) { Console.Write("|"); Console.WriteLine(); }
                }
                else
                {
                    Console.Write($"|Spot {i}:");

                    foreach (var vehicle in parkingSpots[i])
                    {
                        if (vehicle.Type == VehicleType.Car)
                        {
                            AnsiConsole.Markup($"[blue] {vehicle.RegNumber} [/]");
                        }
                        else if (vehicle.Type == VehicleType.Motorcycle)
                        {
                            AnsiConsole.Markup($"[red] {vehicle.RegNumber} [/]");
                        }
                    }
                    if (i % 5 == 0) { Console.Write("|"); Console.WriteLine(); }
                }
            }
            Console.WriteLine("\nPress any key to go back to the main menu");
            Console.ReadKey(true);
            Console.Clear();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void UpdateParkingSpots(List<List<ParkingSpot>> spots)
        {
            parkingSpots = spots;
            for (int i = 0; i < parkingSpots.Count; i++)
            {
                if (parkingSpots[i] == null)
                {
                    parkingSpots[i] = new List<ParkingSpot>();
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void AddParkingSpots(List<List<ParkingSpot>> parkingSpots, string filePath)
        {
            Console.WriteLine("How many parking spots would you like to add?");
            if (int.TryParse(Console.ReadLine(), out int addSpots))
            {
                for (int i = 0; i < addSpots; i++)
                {
                    parkingSpots.Add(new List<ParkingSpot>());
                }

                UpdateTotalParkingSpotsInFile(parkingSpots.Count - 1);

                Console.WriteLine($"{addSpots} spots added to the Parking Garage. The updated Parking Garage holds {parkingSpots.Count - 1} spots");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private void UpdateTotalParkingSpotsInFile(int totalSpots)
        {
            // Read all lines from the file
            string filePath = "../../../Konfigfil.txt";
            var lines = File.ReadAllLines(filePath);

            // Update the line that starts with "TotalParkingSpots:"
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("TotalParkingSpots:"))
                {
                    lines[i] = $"TotalParkingSpots: {totalSpots}";
                    break; // Stop after finding the correct line
                }
            }

            // Write the updated lines back to the file
            File.WriteAllLines(filePath, lines);
        }
    }
}
