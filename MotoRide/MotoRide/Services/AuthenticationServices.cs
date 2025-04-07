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

namespace MotoRide.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _respon;
        private readonly ICustomerServices _customerServices;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationServices(MotoRideDbContext context, ServiceResponse response, ICustomerServices customerServices, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _respon = response;
            _customerServices = customerServices;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse> RegisterCustomer(AddCustomerDto dto)
        {
            try
            {
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
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"Cannot Add User: {e.Message}";
            }
            return _respon;
        }
        public async Task<ServiceResponse> AddUser(AddCustomerDto dto)
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
            }
            return _respon;
        }

        public async Task<ServiceResponse> RegisterOwnerShop(AddOwnerShopDto dto)
        {
            try
            {
                Store store = new Store()
                {
                    StoreName = dto.Username,
                    Password = dto.Password,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Location = dto.Location,
                    Iamgelicense = dto.Iamgelicense,
                };
                await _context.Stores.AddAsync(store);
                await _context.SaveChangesAsync();
                _respon.Success = true;
                _respon.Message = "User Added Successfully";
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"Cannot Add User: {e.Message}";
            }
            return _respon;
        }

        public async Task<ServiceResponse> Login(LoginDto dto)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.Username == dto.Username && x.Password == dto.Password);
            if (customer == null)
            {
                var store = _context.Stores.FirstOrDefault(x => x.StoreName == dto.Username && x.Password == dto.Password);
                if (store == null)
                {
                    _respon.Success = false;
                    _respon.Message = "User not found";
                    return _respon;
                }
                else
                {
                    if (store.IsCanLogin == false)
                    {
                        _respon.Success = false;
                        _respon.Message = "You are not allowed to login";
                        return _respon;
                    }
                    else
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
                }
            }
            else
            {
                int CustomerId;
                string token = GenerateJwtToken(dto.Username, CustomerId = customer.UserId, "Customer");
                customer.Token = token;
                await _context.SaveChangesAsync();
                await _customerServices.AddPoints(CustomerId, 5);

                // Set the token in the session
                _httpContextAccessor.HttpContext.Session.SetString("JwtToken", token);

                _respon.Success = true;
                _respon.Message = "User Found";
                _respon.Data = new { CustomerId, Token = token, Type = "Customer" };
                return _respon;
            }
        }

        public async Task<ServiceResponse> GetProfile(int id)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (customer != null)
            {
                _respon.Success = true;
                _respon.Message = "Profile retrieved successfully";
                _respon.Data = customer;
                return _respon;
            }

            var shopOwner = await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == id);
            if (shopOwner != null)
            {
                _respon.Success = true;
                _respon.Message = "Profile retrieved successfully";
                _respon.Data = shopOwner;
            }
            else
            {
                _respon.Success = false;
                _respon.Message = "User not found";
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

    }
}
