using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello__world_Prague_parking_v3._0
{
    public enum VehicleType      // flyttad till egen klass hade svårt med kallelse gjorde privat så inget kan störa den
    {
        Car,           // Worth 4 points
        Motorcycle,    // Worth 2 points
        Bike,          // Worth 1 point
        Bus,           // Worth 16 points
        Not_Parked     // Default value
    }
}
