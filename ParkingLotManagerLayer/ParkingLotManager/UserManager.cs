using ParkingLotManagerLayer.IParkingLotManager;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IParkingRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManagerLayer.ParkingLotManager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;
        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserDetails LoginUser(LoginModel loginModel)
        {
            return this.userRepository.LoginUser(loginModel);
        }

        public Task<int> RegisterUser(UserDetails userDetails)
        {
            return this.userRepository.RegisterUser(userDetails);
        }
        public Task<int> AddNewDriverType(DriverTypeDetails driverTypeDetails)
        {
            return this.userRepository.AddNewDriverType(driverTypeDetails);
        }

        public Task<int> AddNewVehicleType(VehicleTypeDetails vehicleTypeDetails)
        {
            return this.userRepository.AddNewVehicleType(vehicleTypeDetails);
        }
        public Task<int> AddNewParkingType(ParkingTypeDetails parkingTypeDetails)
        {
            return this.userRepository.AddNewParkingType(parkingTypeDetails);
        }
    }
}
