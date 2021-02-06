using ParkingLotManagerLayer.IParkingLotManager;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IParkingRepository;
using ParkingLotRepositoryLayer.ParkingRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManagerLayer.ParkingLotManager
{
    public class ParkingManager : IParkingManager
    {
        private readonly IParkingRepo parkingRepo;
        public ParkingManager(IParkingRepo parkingRepo)
        {
            this.parkingRepo = parkingRepo;
        }

        public Task<int> Parking(ParkingDetails parkingDetails)
        {
            return parkingRepo.Parking(parkingDetails);
        }
        public object UnParking(int slotNumber)
        {
            return parkingRepo.UnParking(slotNumber);
        }

        public Task<int> DeleteAllUnParkedVehicles()
        {
            return parkingRepo.DeleteAllUnParkedVehicles();
        }
       
        public ParkingDetails SearchBySlotNumber(int slotNumber)
        {
            return parkingRepo.SearchBySlotNumber(slotNumber);
        }

        public ParkingDetails SearchByVehicleNumber(string vehicleNumber)
        {
            return parkingRepo.SearchByVehicleNumber(vehicleNumber);
        }
        public IEnumerable<ParkingDetails> GetAllParkingDetails()
        {
            return parkingRepo.GetAllParkingDetails();
        }
    }
}
