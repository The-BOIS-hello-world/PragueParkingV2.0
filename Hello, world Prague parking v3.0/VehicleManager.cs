using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hello__world_Prague_parking_v3._0
{
    public class VehicleManager
    {
        //metod checkfor symbol and already exisiting regnumber
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public bool CharCheck(string regNumber)
        {
            bool hasSymbol = regNumber.Any(c => !char.IsLetterOrDigit(c));
            return hasSymbol;
        }
        public bool RegExists(string regNumber, List<List<ParkingSpot>> parkingSpots)
        {
            for (int i = 1; i < parkingSpots.Count; i++)
            {
                for (int j = 0; j < parkingSpots[i].Count; j++)
                {
                    if (parkingSpots[i][j].RegNumber == regNumber)
                    {
                        Console.WriteLine("Reg number already exists ");
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsValidRegistration(string regNumber, List<List<ParkingSpot>> parkingSpots)
        {
            return regNumber.Length >= 4 && regNumber.Length <= 10
                && !string.IsNullOrEmpty(regNumber)
                && !CharCheck(regNumber)
                && !RegExists(regNumber, parkingSpots);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //AddVehicle
        public void AddVehicle(List<List<ParkingSpot>> spots, int vehicleChoice)                       // Method to add a car
        {
            Console.Write("Please enter the registration number: ");
            string regNumber = Console.ReadLine()?.ToUpper();

            while (!IsValidRegistration(regNumber, spots))
            {
                Console.WriteLine(@"Reg is invalid. Retry or type ""Return"" to go to main menu:");
                Console.Write("Enter your registration number: ");
                regNumber = Console.ReadLine().ToUpper();

                if (regNumber == "RETURN") return;
            }
            if (vehicleChoice == 1)
            {
                int availableSpot = Car.FindAvailableSpot(spots);
                if (availableSpot == -1)
                {
                    Console.WriteLine("No available spots for a car, you will now return to the mainn menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                }

                ParkingSpot newCar = new ParkingSpot(regNumber, VehicleType.Car, availableSpot);
                spots[availableSpot].Add(newCar);
                Console.WriteLine($"Car with registration number {regNumber} parked at spot {availableSpot}.");
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (vehicleChoice == 2)
            {

                int availableSpot = Mc.FindAvailableSpot(spots);
                if (availableSpot == -1)
                {
                    Console.WriteLine("No available spots for a motorcycle you will now return to the main menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                }

                ParkingSpot newMc = new ParkingSpot(regNumber, VehicleType.Motorcycle, availableSpot);
                spots[availableSpot].Add(newMc);
                Console.WriteLine($"Motorcycle with registration number {regNumber} parked at spot {availableSpot}.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
        //RemoveVehicle
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void RemoveVehicle(List<List<ParkingSpot>> spots, int vehicleChoice)
        {
            Console.Write("Please enter the registration number: ");
            string regNumber = Console.ReadLine().ToUpper();

            if (vehicleChoice == 1)
            {
                for (int i = 1; i < spots.Count; i++)
                {
                    var currentSpot = spots[i];

                    if (currentSpot != null && currentSpot.Any())
                    {
                        var carToRemove = currentSpot.FirstOrDefault(vehicle => vehicle.RegNumber == regNumber && vehicle.Type == VehicleType.Car);

                        if (carToRemove != null)
                        {
                            carToRemove.setTimeOut();          // Set the time the vehicle left
                            carToRemove.CalculateCharge();     // Calculate and display the parking charge
                            currentSpot.Remove(carToRemove);
                            Console.WriteLine($"Car with registration number {regNumber} removed from spot {i}.");
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey(true);
                            Console.Clear();
                            return;
                        }
                    }
                }
            }
            else if (vehicleChoice == 2)
            {
                for (int i = 1; i < spots.Count; i++)
                {
                    var currentSpot = spots[i];

                    if (currentSpot != null && currentSpot.Any())
                    {
                        var mcToRemove = currentSpot.FirstOrDefault(vehicle => vehicle.RegNumber == regNumber && vehicle.Type == VehicleType.Motorcycle);

                        if (mcToRemove != null)
                        {
                            mcToRemove.setTimeOut();          // Set the time the vehicle left
                            mcToRemove.CalculateCharge();     // Calculate and display the parking charge
                            currentSpot.Remove(mcToRemove);
                            Console.WriteLine($"Motorcycle with registration number {regNumber} removed from spot {i}.");
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey(true);
                            Console.Clear();
                            return;
                        }
                    }
                }
                Console.WriteLine($"Car with registration number {regNumber} was not found in the parking garage.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //MoveVehicle
        public void MoveVehicle(List<List<ParkingSpot>> spots, int vehicleChoice)
        {
            int i = 1;
            Console.Write("Please enter the registration number: ");
            string regNumber = Console.ReadLine()?.ToUpper();

            if (vehicleChoice == 1)
            {
                for (i = 1; i < spots.Count; i++)
                {
                    var currentSpot = spots[i];

                    if (currentSpot != null && currentSpot.Any())
                    {
                        var carmove = currentSpot.FirstOrDefault(vehicle => vehicle.RegNumber == regNumber && vehicle.Type == VehicleType.Car);

                        if (carmove != null)
                        {
                            Console.Write($"Your car was found in spot {i}, which spot would you like to move to? ");
                            break;
                        }
                    }
                    if (i == spots.Count - 1)
                    {
                        Console.WriteLine("A car with this Regnumber was not found you will be returned to the main menu");
                        Console.ReadKey(true);
                        Console.Clear();
                        return;
                    }
                }

                int spotIndex;
                if (int.TryParse(Console.ReadLine(), out spotIndex) && spotIndex >= 1 && spotIndex <= spots.Capacity);
                else
                {
                    Console.WriteLine("Invalid input you will be returned to the main menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                }

                if (spotIndex <= spots.Count && spotIndex >= 1)
                {
                    if (Car.IsAvailableSpot(spots, spotIndex))
                    {
                        Car.Move(spots, spotIndex, regNumber, i);
                        Console.WriteLine($"Your Car was moved to spot number {spotIndex}");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    return;
                }
            }
            if (vehicleChoice == 2)
            {
                for (i = 1; i < spots.Count; i++)
                {
                    var currentSpot = spots[i];

                    if (currentSpot != null && currentSpot.Any())
                    {
                        var carmove = currentSpot.FirstOrDefault(vehicle => vehicle.RegNumber == regNumber && vehicle.Type == VehicleType.Motorcycle);

                        if (carmove != null)
                        {
                            Console.Write($"Your Motorcycle was found in spot {i}, which spot would you like to move to? ");
                            break;
                        }
                    }
                    if (i == spots.Count - 1)
                    {
                        Console.WriteLine("A Motorcycle with this Regnumber was not found you will be returned to the main menu");
                        Console.ReadKey(true);
                        Console.Clear();
                        return;
                    }
                }

                int spotIndex;
                if (int.TryParse(Console.ReadLine(), out spotIndex) && spotIndex >= 1 && spotIndex <= spots.Count) ;
                else
                {
                    Console.WriteLine("Invalid input you will be returned to the main menu");
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                }

                if (spotIndex <= spots.Count && spotIndex >= 1)
                {
                    if (Car.IsAvailableSpot(spots, spotIndex))
                    {
                        Mc.Move(spots, spotIndex, regNumber, i);
                        Console.WriteLine($"Your Car was moved to spot number {spotIndex}");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    return;
                }
            }
        }
    }
}
