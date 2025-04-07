using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Services
{
    public class StoreServices : IStoreServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _respon;


        public StoreServices(MotoRideDbContext context, ServiceResponse response)
        {
            _context = context;
            _respon = response;

        }
        public async Task<ServiceResponse> GetAllStore()
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

        public async Task<ServiceResponse> AllowStoreToLogin(int id,bool IsCanLogin)
        {
            try {
                var store = await _context.Stores.FirstOrDefaultAsync(x => x.StoreId == id);
                if (store == null)
                {
                    _respon.Success = false;
                    _respon.Message = "can not allow login for this store";
                }
                else
                {
                    store.IsCanLogin = IsCanLogin;
                    _context.Stores.Update(store);
                    await _context.SaveChangesAsync();
                    _respon.Success = true;
                    _respon.Message = "done to allow login for this store";
                }
                return _respon;
            }
            catch(Exception ex)
            {
                _respon.Success = false;
                _respon.Message = "can not allow login for this store"+ex.Message;
                return _respon;
            }
        }
    }
}
