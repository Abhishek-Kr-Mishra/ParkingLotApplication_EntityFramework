using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IParkingRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotRepositoryLayer.ParkingRepository
{
    public class ParkingRepo : IParkingRepo
    {
        private readonly ApplicationDbContext applicationDbContext;
        public ParkingRepo(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public Task<int> Parking(ParkingDetails parkingDetails)
        {
            ParkingDetails details = new ParkingDetails()
            {
                VehicleNumber = parkingDetails.VehicleNumber,
                EntryTime = parkingDetails.EntryTime,
                ParkingType = parkingDetails.ParkingType,
                VehicleType = parkingDetails.VehicleType,
                DriverType = parkingDetails.DriverType,
                ExitTime = parkingDetails.ExitTime,
                ParkingSlotNumber = parkingDetails.ParkingSlotNumber,
                Charges = parkingDetails.Charges,
                IsEmpty = false
            };
            this.applicationDbContext.ParkingDetails.Add(details);
            var result = this.applicationDbContext.SaveChangesAsync();
            return result;
        }
        public object UnParking(int slotNumber)
        {
            var parkingResult = this.applicationDbContext.ParkingDetails.Where<ParkingDetails>(details => details.ParkingSlotNumber == slotNumber
                                && details.IsEmpty == false).FirstOrDefault();
            var result = CalculateCharge(parkingResult.ParkingID);

            var exitTime = DateTime.Now;
            parkingResult.ExitTime = exitTime;
            parkingResult.IsEmpty = true;
            this.applicationDbContext.ParkingDetails.Update(parkingResult);
            this.applicationDbContext.SaveChangesAsync();
            return result;
        }
        public ParkingDetails SearchByVehicleNumber(string vehicleNumber)
        {
            var parkingResult = this.applicationDbContext.ParkingDetails.Where<ParkingDetails>(details => details.VehicleNumber == vehicleNumber).FirstOrDefault();
            return parkingResult;
        }
        public ParkingDetails SearchBySlotNumber(int slotNumber)
        {
            var parkingResult = this.applicationDbContext.ParkingDetails.Where<ParkingDetails>(details => details.ParkingSlotNumber == slotNumber).FirstOrDefault();
            return parkingResult;
        }
        public Task<int> DeleteAllUnParkedVehicles()
        {
            var result = this.applicationDbContext.ParkingDetails.Where<ParkingDetails>(details => details.IsEmpty == true).ToList<ParkingDetails>();
            foreach(var data in result)
            {
                this.applicationDbContext.ParkingDetails.Remove(data);
            }
            var deleteStatus = this.applicationDbContext.SaveChangesAsync();
            return deleteStatus;
        }
        public IEnumerable<ParkingDetails> GetAllParkingDetails()
        {
            var result = this.applicationDbContext.ParkingDetails.Where<ParkingDetails>(details => details.IsEmpty == false).ToList<ParkingDetails>();
            return result;
        }
        public object CalculateCharge(int parkingID)
        {
            var result = from parkingDetails in applicationDbContext.ParkingDetails
                         join parkingTypeDetails in applicationDbContext.ParkingTypeDetails
                         on parkingDetails.ParkingType equals parkingTypeDetails.ParkingTypeID
                         join driverTypeDetails in applicationDbContext.DriverTypeDetails
                         on parkingDetails.DriverType equals driverTypeDetails.DriverTypeID
                         join vehicleTypeDetails in applicationDbContext.VehicleTypeDetails
                         on parkingDetails.VehicleType equals vehicleTypeDetails.VehicleTypeID
                         select new ParkingResponse()
                         {
                             ParkingID = parkingDetails.ParkingID,
                             VehicleNumber = parkingDetails.VehicleNumber,
                             EntryTime = parkingDetails.EntryTime,
                             ParkingType = parkingTypeDetails.ParkingType,
                             DriverType = driverTypeDetails.DriverType,
                             VehicleType = vehicleTypeDetails.VehicleType,
                             ExitTime = System.DateTime.Now,
                             ParkingSlotNumber = parkingDetails.ParkingSlotNumber,
                             IsEmpty = parkingDetails.IsEmpty,
                             Charges = parkingDetails.Charges,
                             ParkingCharges = parkingTypeDetails.Charges,
                             VehicleCharges = vehicleTypeDetails.VehicleCharges,
                             DriverCharges = driverTypeDetails.DriverCharges
                         };
            foreach(var data in result)
            {
                if(data.ParkingID == parkingID)
                {
                    return data;
                }
            }
            return null;
        }
    }
}
