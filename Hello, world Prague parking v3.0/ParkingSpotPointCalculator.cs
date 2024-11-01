using Hello__world_Prague_parking_v3._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello__world_Prague_parking_v3._0
{
    internal class ParkingSpotPointCalculator
    {
        // --------Mc--------
        public static int CalculateTotalPointsMc(List<List<ParkingSpot>> spots)
        {
            for (int i = 1; i < spots.Count; i++)
            {
                int totalPoints = 0;
                List<ParkingSpot> spotList = spots[i];

                if (spotList == null || spotList.Count == 0)
                {
                    return i;  // Return the index of the null or empty list
                }

                // Loop through each ParkingSpot in the inner list using a for loop
                for (int j = 0; j < spotList.Count; j++)
                {
                    ParkingSpot spot = spotList[j];

                    totalPoints += spot.Points;
                    if ((j == spotList.Count - 1) && (totalPoints <= ParkingGarage.LoadSpotSizeFromConfig() - ParkingGarage.LoadMCSizeFromConfig()))
                    {
                        return i;
                    }
                    if (totalPoints == 4)
                    {
                        totalPoints = 0;
                        break;
                    }
                }
            }
            return -1;
        }
        // ------Car--------
        public static int CalculateTotalPointsCar(List<List<ParkingSpot>> spots)
        {
            for (int i = 1; i < spots.Count; i++)
            {
                int totalPoints = 0;
                List<ParkingSpot> spotList = spots[i];

                if (spotList == null || spotList.Count == 0)
                {
                    return i;  // Return the index of the null or empty list
                }

                // Loop through each ParkingSpot in the inner list using a for loop
                for (int j = 0; j < spotList.Count; j++)
                {
                    ParkingSpot spot = spotList[j];

                    totalPoints += spot.Points;
                    if ((j == spotList.Count - 1) && (totalPoints <= ParkingGarage.LoadSpotSizeFromConfig() - ParkingGarage.LoadCarSizeFromConfig()))
                    {
                        return i;
                    }
                    if (totalPoints == 4)
                    {
                        totalPoints = 0;
                        break;
                    }
                }
            }
            return -1;
        }
    }
}