using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotRepositoryLayer.IParkingRepository
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(UserDetails userDetails);
        UserDetails LoginUser(LoginModel loginModel);
        Task<int> AddNewDriverType(DriverTypeDetails driverTypeDetails);
        Task<int> AddNewVehicleType(VehicleTypeDetails vehicleTypeDetails);
        Task<int> AddNewParkingType(ParkingTypeDetails parkingTypeDetails);
    }
}
