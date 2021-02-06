using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotModelLayer
{
    public class ParkingResponse
    {
        public int ParkingID { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime EntryTime { get; set; }
        public string ParkingType { get; set; }
        public string VehicleType { get; set; }
        public string DriverType { get; set; }
        public DateTime ExitTime { get; set; }
        public int ParkingSlotNumber { get; set; }
        public double Charges { get; set; }
        public bool IsEmpty { get; set; }
        public double ParkingCharges { get; set; }
        public double DriverCharges { get; set; }
        public double VehicleCharges { get; set; }
    }
}
