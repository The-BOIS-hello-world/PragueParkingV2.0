using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Hello__world_Prague_parking_v3._0
{
    public static class Json
    {
        public static List<List<ParkingSpot>> spots = InitializeParkingStructure();

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static List<List<ParkingSpot>> InitializeParkingStructure() // Initializes parking structure with spots based on configuration
        {
            int totalSpots = LoadTotalSpotsFromConfig() +1;
            var parkingSpots = new List<List<ParkingSpot>>(new List<ParkingSpot>[totalSpots]);

            for (int i = 0; i < totalSpots; i++)
            {
                parkingSpots[i] = new List<ParkingSpot>(); // Create a nested list for each spot
            }
            return parkingSpots;
        }

        private static int LoadTotalSpotsFromConfig()
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
            return 101; // Default if config value not found
        }
        public static void SaveToJson(List<List<ParkingSpot>> spots, string filePath)                   // Saves parking spots to a JSON file
        {
            var flatList = new List<ParkingSpot>();                                                     // maipulerar nested list så att json kan spara all info och behålla data i nested listor 

            for (int i = 0; i < spots.Count; i++)
            {
                if (spots[i] != null)                                                            //går egenom varje index av listan och sparar individuellt readall va inte tillräckligt detalierat
                {
                    foreach (var spot in spots[i])
                    {
                        spot.SpotIndex = i;
                        flatList.Add(spot);                                                             // saves all nested list with index point to find easier when d 
                    }
                }
            }

            string jsonString = JsonSerializer.Serialize(flatList);                                               // samma som det gamla
            File.WriteAllText(filePath, jsonString);
            AnsiConsole.MarkupLine("[green]Parking spots saved successfully.[/]");
            Console.ReadKey(true);
            Console.Clear();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static List<List<ParkingSpot>> LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                AnsiConsole.MarkupLine("[yellow]File not found. Starting with a new parking lot.[/]");
                Console.ReadKey(true);
                Console.Clear();
                return InitializeParkingStructure();
            }

            string jsonString = File.ReadAllText(filePath);
            var flatList = JsonSerializer.Deserialize<List<ParkingSpot>>(jsonString);

            int maxSpotIndex = LoadTotalSpotsFromConfig() + 1;

            foreach (var spot in flatList)
            {
                if (spot.SpotIndex > maxSpotIndex)
                {
                    maxSpotIndex = spot.SpotIndex;
                }
            }

            var spots = new List<List<ParkingSpot>>(new List<ParkingSpot>[maxSpotIndex + 1]);
            for (int i = 0; i <= maxSpotIndex; i++)
            {
                spots[i] = new List<ParkingSpot>();
            }
            foreach (var spot in flatList)
            {
                spots[spot.SpotIndex].Add(spot);
            }

            Console.Clear();
            return spots;
        }
    }
}
