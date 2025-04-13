using Azure;
using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MotoRide.Helper.Enum;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace MotoRide.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _respon;
        private readonly ICustomerServices _customerServices;
        private readonly IConfiguration _config;
        private readonly ImageServices _image;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationServices(MotoRideDbContext context, ServiceResponse response, ICustomerServices customerServices, IConfiguration config, IHttpContextAccessor httpContextAccessor, ImageServices image)
        {
            _context = context;
            _respon = response;
            _customerServices = customerServices;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _image = image;
        }

        public async Task<ServiceResponse> RegisterCustomer(AddCustomerDto dto)
        {
            try
            {
                if(DoesUsernameAndPasswordExist(dto.Username))
                {
                    _respon.Success = false;
                    _respon.Message = $"Username are Exist";
                }
                else { 
                User user = new User()
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    UserType = "Customer"
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                _respon.Success = true;
                _respon.Message = "User Added Successfully";
            }
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"Cannot Add User: {e.Message}";
            }
            return _respon;
        }
        public async Task<ServiceResponse> AddUser(AddCustomerDto dto)
        {
            if (DoesUsernameAndPasswordExist(dto.Username))
            {
                _respon.Success = false;
                _respon.Message = $"Username are Exist";
            }
            else
            {
                try
                {
                    User user = new User()
                    {
                        Username = dto.Username,
                        Password = dto.Password,

                        UserType = dto.UserType
                    };
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    _respon.Success = true;
                    _respon.Message = "User Added Successfully";
                }
                catch (Exception e)
                {
                    _respon.Success = false;
                    _respon.Message = $"Cannot Add User: {e.Message}";
                } }
                return _respon;
           
        }

        public async Task<ServiceResponse> RegisterOwnerShop(AddOwnerShopDto dto)
        {
            try
            {
                string? Iamgelicense = null;

                // Check if a product image file is provided
                if (Iamgelicense != null)
                {
                    // Save the image and get its path
                    Iamgelicense = await _image.Imges(dto.Iamgelicense);
                }

                if (DoesUsernameAndPasswordExist(dto.Username))
                {
                    _respon.Success = false;
                    _respon.Message = $"Username are Exist";
                }
                else
                {
                    Store store = new Store()
                    {
                        StoreName = dto.Username,
                        Password = dto.Password,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        Location = dto.Location,
                        Iamgelicense = Iamgelicense,
                    };
                    await _context.Stores.AddAsync(store);
                    await _context.SaveChangesAsync();
                    _respon.Success = true;
                    _respon.Message = "User Added Successfully";
                }
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"Cannot Add User: {e.Message}";
            }
            return _respon;
        }
        public async Task<ServiceResponse> RegisterMaintenance(AddMaintenanceDto dto)
        {
            try
            {
                string? Iamgelicense = null;

                // Check if a product image file is provided
                if (Iamgelicense != null)
                {
                    // Save the image and get its path
                    Iamgelicense = await _image.Imges(dto.Iamgelicense);
                }

                if (DoesUsernameAndPasswordExist(dto.MaintenanceName))
                {
                    _respon.Success = false;
                    _respon.Message = $"Username are Exist";
                }
                else
                {
                    Maintenance maintenance = new Maintenance()
                    {
                        MaintenanceName = dto.MaintenanceName,
                        Password = dto.Password,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        Location = dto.Location,
                        Iamgelicense = Iamgelicense,
                    };
                    await _context.Maintenances.AddAsync(maintenance);
                    await _context.SaveChangesAsync();
                    _respon.Success = true;
                    _respon.Message = "Maintenance Added Successfully";
                }
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"Cannot Add Maintenance: {e.Message}";
            }
            return _respon;
        }
        public async Task<ServiceResponse> Login(LoginDto dto)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.Username == dto.Username && x.Password == dto.Password);
            if (customer == null)
            {
             await LoginShop(dto);
                return _respon;
            }
            else
            {
                if (customer.UserType == "delivery")
                {
                    int deliveryId;
                    string token = GenerateJwtToken(dto.Username, deliveryId = customer.UserId, "delivery");
                    customer.Token = token;
                    await _context.SaveChangesAsync();

                    // Set the token in the session
                    _httpContextAccessor.HttpContext.Session.SetString("JwtToken", token);

                    _respon.Success = true;
                    _respon.Message = "Oops! Your username or password is incorrect. Please try again.";
                    _respon.Data = new { deliveryId, Token = token, Type = "delivery" };
                    return _respon;
                }

                if (customer.UserType == "admin")
                {
                    int adminId;
                    string token = GenerateJwtToken(dto.Username, adminId = customer.UserId, "admin");
                    customer.Token = token;
                    await _context.SaveChangesAsync();

                    // Set the token in the session
                    _httpContextAccessor.HttpContext.Session.SetString("JwtToken", token);

                    _respon.Success = true;
                    _respon.Message = "Oops! Your username or password is incorrect. Please try again.";
                    _respon.Data = new { adminId, Token = token, Type = "admin" };
                    return _respon;
                }
                else { 
                int CustomerId;
                string token = GenerateJwtToken(dto.Username, CustomerId = customer.UserId, "Customer");
                customer.Token = token;
                await _context.SaveChangesAsync();
                await _customerServices.AddPoints(CustomerId, 5);

                // Set the token in the session
                _httpContextAccessor.HttpContext.Session.SetString("JwtToken", token);

                _respon.Success = true;
                _respon.Message = "Oops! Your username or password is incorrect. Please try again.";
                _respon.Data = new { CustomerId, Token = token, Type = "Customer" };
                return _respon;
            }}
        }
        private async Task<ServiceResponse> LoginMaintenance(LoginDto dto)
        {
            var maintenance = await _context.Maintenances.FirstOrDefaultAsync(x => x.MaintenanceName == dto.Username && x.Password == dto.Password);
            if (maintenance == null)
            {
                _respon.Success = false;
                _respon.Message = "Oops! Your username or password is incorrect. Please try again.";
                return _respon;
            }
            else
            {
                if (maintenance.IsCanLogin == true)
                {
                    string token = GenerateJwtToken(dto.Username, maintenance.Id, "maintenance");
                    maintenance.Token = token;
                    await _context.SaveChangesAsync();

                    // Set the token in the session
                    _httpContextAccessor.HttpContext.Session.SetString("JwtToken", token);

                    _respon.Success = true;
                    _respon.Message = "maintenance Found";
                    _respon.Data = new { maintenanceId = maintenance.Id, Token = token, Type = "maintenance" };
                    return _respon;
                }
                else
                {
                    _respon.Success = false;
                    _respon.Message = "You can't log in just yet — we're still reviewing your account. Please check your email soon!";
                    return _respon;

                }
            }

        }
        private async Task<ServiceResponse> LoginShop(LoginDto dto)
        {
            var store = _context.Stores.FirstOrDefault(x => x.StoreName == dto.Username && x.Password == dto.Password);
            if (store == null)
            {
                await LoginMaintenance(dto);
                return _respon;
            }
            else
            {
                if (store.IsCanLogin == true)
                {
                    string token = GenerateJwtToken(dto.Username, store.StoreId, "Shop");
                    store.Token = token;
                    await _context.SaveChangesAsync();

                    // Set the token in the session
                    _httpContextAccessor.HttpContext.Session.SetString("JwtToken", token);

                    _respon.Success = true;
                    _respon.Message = "ShopOwner Found";
                    _respon.Data = new { ShopId = store.StoreId, Token = token, Type = "Shop" };
                  
                    return _respon;
                }
                else 
                {
                    _respon.Success = false;
                    _respon.Message = "You can't log in just yet — we're still reviewing your account. Please check your email soon!";
                    return _respon;

                }
            }
        
        }
        public async Task<ServiceResponse> GetCustomerProfile(int id)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (customer != null)
            {
                _respon.Success = true;
                _respon.Message = "Profile retrieved successfully";
                _respon.Data = customer;
                return _respon;
            }


            return _respon;
        }

        public async Task<ServiceResponse> Logout(int id)
        {
            string? token;
            var customer = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (customer == null)
            {
                var shopOwner = _context.Stores.FirstOrDefault(x => x.StoreId == id);
                if (shopOwner == null)
                {
                    _respon.Success = false;
                    _respon.Message = "User not found";
                    return _respon;
                }
                else
                {
                    token = null;
                    shopOwner.Token = token;
                    await _context.SaveChangesAsync();

                    _httpContextAccessor.HttpContext.Session.Clear();
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");

                    _respon.Success = true;
                    _respon.Message = "User logged out successfully";
                    return _respon;
                }
            }
            else
            {
                token = null;
                customer.Token = token;
                await _context.SaveChangesAsync();

                _httpContextAccessor.HttpContext.Session.Clear();
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");

                _respon.Success = true;
                _respon.Message = "User logged out successfully";
                return _respon;
            }
        }

        private string GenerateJwtToken(string username, int userId, string role)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");

            if (string.IsNullOrEmpty(role))
                throw new ArgumentNullException(nameof(role), "Role cannot be null or empty.");

            if (string.IsNullOrEmpty(_config["Jwt:Key"]))
                throw new ArgumentNullException((_config["Jwt:Key"]), "JWT key cannot be null or empty.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim("id", userId.ToString()),
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResponse> ResetPassword(string email, string newPassword)
        {
            try
            {
                // Check if the email belongs to a Customer
                var customer = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (customer != null)
                {
                    customer.Password = newPassword; // Update the password
                    await _context.SaveChangesAsync();
                    _respon.Success = true;
                    _respon.Message = "Password reset successfully";
                    return _respon;
                }

                // Check if the email belongs to a ShopOwner
                var shopOwner = await _context.Stores.FirstOrDefaultAsync(x => x.Email == email);
                if (shopOwner != null)
                {
                    shopOwner.Password = newPassword; // Update the password
                    await _context.SaveChangesAsync();
                    _respon.Success = true;
                    _respon.Message = "Password reset successfully";
                    return _respon;
                }

                // If no user was found with the provided email
                _respon.Success = false;
                _respon.Message = "User not found with the provided email";
                return _respon;
            }
            catch (Exception e)
            {
                // Handle any exceptions that may occur during the process
                _respon.Success = false;
                _respon.Message = $"An error occurred while resetting the password: {e.Message}";
                return _respon;
            }
        }
        private bool DoesUsernameAndPasswordExist(string username)
        {
            bool existsInStore = _context.Stores.Any(x => x.StoreName == username);
            bool existsInUsers = _context.Users.Any(x => x.Username == username);
            bool existsInMaintenance = _context.Maintenances.Any(x => x.MaintenanceName == username);

            return existsInStore || existsInUsers || existsInMaintenance;
        }


    }
}
