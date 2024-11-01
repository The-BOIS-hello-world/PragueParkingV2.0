using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hello__world_Prague_parking_v3._0
{
    public class Car : ParkingSpot
    {
        public Car(string regNumber) : base(regNumber, VehicleType.Car) { }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //FindAvalibleSpotCar
        public static int FindAvailableSpot(List<List<ParkingSpot>> spots)                                      // Helper method to find an available spot for a car
        {
            return ParkingSpotPointCalculator.CalculateTotalPointsCar(spots);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //IsAvailableSpotCar
        public static bool IsAvailableSpot(List<List<ParkingSpot>> parkingSpots, int spotIndex)
        {
            if (parkingSpots[spotIndex].Count == 0)
            {
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void Move(List<List<ParkingSpot>> parkingSpots, int spotIndex, string regNumber, int i)
        {
            ParkingSpot newCar = new ParkingSpot(regNumber, VehicleType.Car, spotIndex);
            parkingSpots[spotIndex].Add(newCar);

            var carToRemove = parkingSpots[i].FirstOrDefault(vehicle => vehicle.RegNumber == regNumber && vehicle.Type == VehicleType.Car);

            if (carToRemove != null)
            {
                parkingSpots[i].Remove(carToRemove);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
    }
}