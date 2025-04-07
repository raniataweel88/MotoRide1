using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.IServices;
using MotoRide.Models;
using static MotoRide.Helper.Enum;

namespace MotoRide.Services
{
    public class CustomerServices:ICustomerServices

    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _respon;
        public CustomerServices(MotoRideDbContext context,ServiceResponse response)
        {
            _context = context;
            _respon = response;
        }
        private async Task<ServiceResponse> UpdateUserLevel(int userId)
        {
            try
            {
                var customer = await _context.Users.FindAsync(userId);
                if (customer == null)
                {
                    _respon.Success = false;
                    _respon.Message = "User Not Found";
                }

                if (customer.Points >= 1000)
                    customer.Level = UserLevel.VIP;
                else if (customer.Points >= 500)
                    customer.Level = UserLevel.Pro;
                else
                    customer.Level = UserLevel.Basic;

                _context.Users.Update(customer);
                await _context.SaveChangesAsync();
                return _respon;
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"can not Update User{e.Message}";
                return _respon;
            }
        }
        public async Task<ServiceResponse> AddPoints(int userId, int points)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _respon.Success = false;
                    _respon.Message = "User Not Found";
                }

                user.Points += points;
                await UpdateUserLevel(userId);
                await _context.SaveChangesAsync();
                _respon.Success = true;
                _respon.Message = "Points Added Successfully";
                return _respon;
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"can not Add Points{e.Message}";
                return _respon;
            }
        }
        public async Task<ServiceResponse> GetLevel(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _respon.Success = false;
                    _respon.Message = "User Not Found";
                }
                _respon.Success = true;
                _respon.Data = user.Level.ToString();
                return _respon;
            }
            catch (Exception e)
            {
                _respon.Success = false;
                _respon.Message = $"can not Get Level{e.Message}";
                return _respon;
            }
        }
    }
}
