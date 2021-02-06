using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotManagerLayer.IParkingLotManager;
using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IConfiguration configuration;

        public UserController(IUserManager userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UserDetails userDetails)
        {
            try
            {
                var password = Encryptdata(userDetails.Password);
                userDetails.Password = password;
                var result = await this.userManager.RegisterUser(userDetails);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "User Registration Sucssesfull", Data = userDetails });
                }
                return this.BadRequest(new { Status = false, Message = "User Registration Un-Sucssesfull" });
            }
            catch(Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = this.userManager.LoginUser(loginModel);
                if (result != null)
                {
                    if(Decryptdata(result.Password) == loginModel.Password)
                    {
                        string token = GenrateJWTToken(loginModel.Email, result.Role);
                        return this.Ok(new { Status = true, Message = "User Loged In Sucssesfull", Data = token });
                    }

                }
                return this.BadRequest(new { Status = false, Message = "User Login Failed" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("AddDriverType")]
        public async Task<ActionResult> AddDriverType(DriverTypeDetails driverTypeDetails)
        {
            try
            {
                var result = await this.userManager.AddNewDriverType(driverTypeDetails);
                if (result == 1)
                {
                    return this.Ok(new { Staus = true, Message = "Driver Type Inserted Sucssesfully", Data = driverTypeDetails });
                }
                return this.BadRequest(new { Staus = false, Message = "Driver Type Insertion Un-sucssesfull" });
            }
            catch(Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("AddVehicleType")]
        public async Task<ActionResult> AddVehicleType(VehicleTypeDetails vehicleTypeDetails)
        {
            try
            {
                var result = await this.userManager.AddNewVehicleType(vehicleTypeDetails);
                if (result == 1)
                {
                    return this.Ok(new { Staus = true, Message = "Vehicle Type Inserted Sucssesfully", Data = vehicleTypeDetails });
                }
                return this.BadRequest(new { Staus = false, Message = "Vehicle Type Insertion Un-sucssesfull" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("AddParkingType")]
        public async Task<ActionResult> AddParkingType(ParkingTypeDetails parkingTypeDetails)
        {
            try
            {
                var result = await this.userManager.AddNewParkingType(parkingTypeDetails);
                if (result == 1)
                {
                    return Ok(new { Status = true, Message = "Parking Type Added Sucssesfull", Data = parkingTypeDetails });
                }
                return this.BadRequest(new { Status = false, Message = "Paking Type Insertion Un-Sucssesfull" });
            }
            catch (Exception e)

            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        private string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        private string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        private string GenrateJWTToken(string email, string Role)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),
                            new Claim(ClaimTypes.Role, Role)

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
    }
}
