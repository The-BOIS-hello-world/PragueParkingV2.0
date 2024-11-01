namespace Hello__world_Prague_parking_v3._0.Tests
{
    [TestClass]
    public class VehicleManagerTests
    {
        [TestMethod]
        public void CharCheck_ShouldReturnTrue_WhenContainsSymbols()
        {
            // Arrange
            string regNumberWithSymbol = "ABC@123";
            VehicleManager vehicleManager = new VehicleManager();

            // Act
            bool result = vehicleManager.CharCheck(regNumberWithSymbol);

            // Assert
            Assert.IsTrue(result, "CharCheck should return true when the registration number contains symbols.");
        }

        [TestMethod]
        public void CharCheck_ShouldReturnFalse_WhenNoSymbols()
        {
            // Arrange
            string regNumberWithoutSymbol = "ABC123";
            VehicleManager vehicleManager = new VehicleManager();

            // Act
            bool result = vehicleManager.CharCheck(regNumberWithoutSymbol);

            // Assert
            Assert.IsFalse(result, "CharCheck should return false when the registration number does not contain symbols.");
        }


        [TestMethod]
        public void RegExists_ShouldReturnFalse_WhenRegNumberDoesNotExist()
        {
            // Arrange
            string nonExistentRegNumber = "XYZ789";
            List<List<ParkingSpot>> parkingSpots = new List<List<ParkingSpot>>
            {
                new List<ParkingSpot>(), // Empty spot for index 0 (if it starts from 1 in your logic)
                new List<ParkingSpot> { new ParkingSpot("ABC123", VehicleType.Car, 1) }
            };
            VehicleManager vehicleManager = new VehicleManager();

            // Act
            bool result = vehicleManager.RegExists(nonExistentRegNumber, parkingSpots);

            // Assert
            Assert.IsFalse(result, "RegExists should return false when the registration number does not exist in the parking spots.");
        }
    }

}




