using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello__world_Prague_parking_v3._0
{
    public class Mc : ParkingSpot
    {
        public Mc(string regNumber) : base(regNumber, VehicleType.Motorcycle) { }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //FindAvalibleSpotMc
        public static int FindAvailableSpot(List<List<ParkingSpot>> spots)       //method to find an available spot for a motorcycle
        {
            return ParkingSpotPointCalculator.CalculateTotalPointsMc(spots);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //MoveMc
        public static void Move(List<List<ParkingSpot>> parkingSpots, int spotIndex, string regNumber, int i)
        {
            ParkingSpot newMc = new ParkingSpot(regNumber, VehicleType.Motorcycle, spotIndex);
            parkingSpots[spotIndex].Add(newMc);

            var McToRemove = parkingSpots[i].FirstOrDefault(vehicle => vehicle.RegNumber == regNumber && vehicle.Type == VehicleType.Motorcycle);

            if (McToRemove != null)
            {
                parkingSpots[i].Remove(McToRemove);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //IsAvailableSpotMC
        public static bool IsAvailableSpot(List<List<ParkingSpot>> spots, int spotIndex)
        {
            int totalPoints = 0;
            List<ParkingSpot> spotList = spots[spotIndex];

            if (spotList == null || spotList.Count == 0)
            {
                return true;  // Return the index of the null or empty list
            }

            // Loop through each ParkingSpot in the inner list using a for loop
            for (int j = 0; j < spotList.Count; j++)
            {
                ParkingSpot spot = spotList[j];

                totalPoints += spot.Points;
                if ((j == spotList.Count - 1) && (totalPoints <= 2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}