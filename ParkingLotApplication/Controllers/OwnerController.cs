using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ParkingLotApplication.MSMQ;
using ParkingLotManagerLayer.IParkingLotManager;
using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Owner,Policemen")]
    public class OwnerController : ControllerBase
    {
        private readonly IParkingManager parkingManager;
        private readonly MSMQService mSMQService = new MSMQService();
        public OwnerController(IParkingManager parkingManager)
        {
            this.parkingManager = parkingManager;
        }
        [HttpPost]
        [Route("Parking")]
        public async Task<ActionResult> VehicleParking(ParkingDetails parkingDetails)
        {
            try
            {
                var result = await parkingManager.Parking(parkingDetails);
                if (result == 1 && parkingDetails.DriverType == 1)
                {
                    this.mSMQService.AddToQueue("Vehicle Parked Sucssesfully...Which vehicle number is " + parkingDetails.VehicleNumber + " in Parking Slot " + parkingDetails.ParkingSlotNumber);
                    return this.Ok(new { Status = true, Message = "Vehicle Parked Sucssesfully", Data = parkingDetails });
                }
                return this.BadRequest(new { Status = true, Message = "Vehicle Parking Un-Sucssesfull!!" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = "False", Message = e.Message });
            }
        }
        [HttpPut]
        [Route("UnParking")]
        public ActionResult VehicleUnParking(int slotNumber)
        {
            try
            {
                var result = parkingManager.UnParking(slotNumber);
                if (result != null)
                {
                    this.mSMQService.AddToQueue("Vehicle Un-Parked Sucssesfully  from slot Number " + slotNumber);
                    return this.Ok(new { Status = true, Message = "Vehicle Un-Parked Sucssesfully", Data = result });
                }
                return this.BadRequest(new { Status = true, Message = "Vehicle Un-Parking was Un-Sucssesfull!!" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
