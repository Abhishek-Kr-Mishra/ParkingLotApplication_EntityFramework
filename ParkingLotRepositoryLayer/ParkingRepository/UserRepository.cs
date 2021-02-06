using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IParkingRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotRepositoryLayer.ParkingRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public Task<int> RegisterUser(UserDetails userDetails)
        {
            this.applicationDbContext.UserDetails.Add(userDetails);
            var result = this.applicationDbContext.SaveChangesAsync();
            return result;
        }
        public UserDetails LoginUser(LoginModel loginModel)
        {
            var result = this.applicationDbContext.UserDetails.Where<UserDetails>(details => details.EmailID == loginModel.Email).FirstOrDefault();
            if(result != null)
            {
                return result;
            }
            return null;
        }
        public Task<int> AddNewDriverType(DriverTypeDetails driverTypeDetails)
        {
            this.applicationDbContext.DriverTypeDetails.Add(driverTypeDetails);
            var result = this.applicationDbContext.SaveChangesAsync();
            return result;
        }

        public Task<int> AddNewVehicleType(VehicleTypeDetails vehicleTypeDetails)
        {
            this.applicationDbContext.VehicleTypeDetails.Add(vehicleTypeDetails);
            var result = this.applicationDbContext.SaveChangesAsync();
            return result;
        }
        public Task<int> AddNewParkingType(ParkingTypeDetails parkingTypeDetails)
        {
            this.applicationDbContext.ParkingTypeDetails.Add(parkingTypeDetails);
            var result = this.applicationDbContext.SaveChangesAsync();
            return result;
        }
    }
}
