using Spectre.Console;
using System.Collections.Generic;


namespace Hello__world_Prague_parking_v3._0
{
    internal class MainMenu
    {
        static void Main(string[] args)
        {
            List<List<ParkingSpot>> spots = Json.InitializeParkingStructure();         // need to call on the json initializeparkingstructure to open up the list filled with "spots"
            ParkingGarage garage = new ParkingGarage(spots);                           // creating a list instance which is updted via spots
            string filePath = "";
          //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            AnsiConsole.Write(new FigletText("Welcome to Prague Parking").Centered().Color(Color.Aquamarine1));        // Starting menu with Spectre.Console for New or Existing
            var initialChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .PageSize(3)
                    .AddChoices(new[] { "Open New", "Open Existing", "Exit" }));

            switch (initialChoice)
            {
                case "Open New":
                    AnsiConsole.MarkupLine("[green]Creating a new parking lot.[/]");  // lite fint ba 
                    Console.WriteLine("What do you want to name the file?: ");
                    filePath = (Console.ReadLine() + ".json");
                    Json.SaveToJson(spots, filePath);
                    break;
                case "Open Existing":
                    AnsiConsole.Write("Please enter file name: ");
                    filePath = (Console.ReadLine()+ ".json");                                    //OPens existing json file via the updated parking after initializeparking has uppdated the empy list with the json file
                    spots = Json.LoadFromJson(filePath);
                    garage.UpdateParkingSpots(spots);

                    AnsiConsole.MarkupLine("[green]Existing parking data loaded successfully.[/]");
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[green]Exiting...[/]");
                    return;
            }


            while (true)
            {
                AnsiConsole.Write(new FigletText("Welcome to Prague Parking").Centered().Color(Color.Aquamarine1));         // Main parking management

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Park Vehicle", "Remove Vehicle", "Move Vehicle", "View Parking", "Add Parkingspots", "Exit"
                        }));

                switch (choice)
                {
                    case "Park Vehicle":
                        CarorMcAdd(spots, filePath);
                        break;
                    case "Remove Vehicle":
                        CarorMcRemove(spots, filePath);
                        break;
                    case "Move Vehicle":                                            
                        MoveVehicle(spots, filePath);
                        break;
                    case "View Parking":
                        garage.ViewParking();
                        break;
                    case "Add Parkingspots":
                        garage.AddParkingSpots(spots, filePath);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Exit":
                        ExitChoice(spots, filePath);
                        Environment.Exit(0);
                        return;
                }
            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            static void CarorMcAdd(List<List<ParkingSpot>> spots, string filePath)     //adding a vehicle menu
            {
                VehicleManager vehicleManager = new VehicleManager();       //Instace of VehicleManager Class

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select vehicle type to park:")
                        .PageSize(12)
                        .AddChoices(new[] { "Car", "Motorcycle", "Return" }));

                switch (choice)
                {
                    case "Car":
                        vehicleManager.AddVehicle(spots, 1);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Motorcycle":
                        vehicleManager.AddVehicle(spots, 2);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Return":
                        return;
                }
            }
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            static void CarorMcRemove(List<List<ParkingSpot>> spots, string filePath)     //adding a vehicle menu
            {
                VehicleManager vehicleManager = new VehicleManager();       //Instace of VehicleManager Class

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select vehicle type to park:")
                        .PageSize(12)
                        .AddChoices(new[] { "Car", "Motorcycle", "Return" }));

                switch (choice)
                {
                    case "Car":
                        vehicleManager.RemoveVehicle(spots, 1);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Motorcycle":
                        vehicleManager.RemoveVehicle(spots, 2);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Return":
                        return;
                }
            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            static void MoveVehicle(List<List<ParkingSpot>> spots, string filePath)
            {
                VehicleManager vehicleManager = new VehicleManager();       

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select vehicle type to move:")
                        .PageSize(12)
                        .AddChoices(new[] { "Car", "Motorcycle", "Return" }));

                switch (choice)
                {
                    case "Car":
                        vehicleManager.MoveVehicle(spots, 1);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Motorcycle":
                        vehicleManager.MoveVehicle(spots, 2);
                        Json.SaveToJson(spots, filePath);
                        break;
                    case "Return":
                        return;
                }
            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            static void ExitChoice(List<List<ParkingSpot>> spots, string filePath)
            {
                var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Would you like to save this version: ")
                            .PageSize(12)
                            .AddChoices(new[] { "Yes", "No", "Return" }));

                switch (choice)
                {
                    case "Yes":
                        Json.SaveToJson(spots, filePath);
                        AnsiConsole.MarkupLine("[green]Data saved successfully. Exiting...[/]");
                        Environment.Exit(0);
                        break;
                    case "No":
                        AnsiConsole.Markup("[red] Okej... Bye -.-[/]");
                        Environment.Exit(0);
                        break;
                    case "Return":
                        return;
                }
            }
        }
    }
}