using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Services
{
    public class ShopServices : IShopServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _respon;


        public ShopServices(MotoRideDbContext context, ServiceResponse response)
        {
            _context = context;
            _respon = response;

        }
        public async Task<ServiceResponse> GetAllShop()
        {
            try
            {
                var shops = from s in await _context.Stores.Where(x => x.IsActive != false).ToListAsync()
                            select new
                            {
                                s.StoreId,
                                s.StoreName,

                            };
                if (shops != null)
                {
                    _respon.Data = shops;
                    _respon.Success = true;
                }
                else
                {
                    _respon.Message = "can not get any shop";
                    _respon.Success = false;
                }
                return _respon;
            }
            catch (Exception ex)
            {
                _respon.Message = ex.Message;
                _respon.Success = false;
                return _respon;
            }
         
        }
    }
}
