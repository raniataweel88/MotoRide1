using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Services
{
    public class WishListServices : IWishListServices
    {
        private readonly MotoRideDbContext _context;

        public WishListServices(MotoRideDbContext context)
        {
            _context = context;
        }

        // 1. Create a new WishList
        public async Task<ServiceResponse> AddWishList(AddWishListDto dto)
        {
            var wishList = new WishList
            {
                CustomerId = dto.CustomerId,
                MotorcycleId = dto.MotorcycleId,
                ProductId = dto.ProductId,
                
                CreatedAt = DateTime.UtcNow
            };



            _context.WishLists.Add(wishList);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "WishList created successfully." };
        }

        // 2. Get a specific WishList by ID
        public async Task<ServiceResponse> GetWishList(int customerId)
        {
            var wishList = await _context.WishLists.Where(w => w.CustomerId == customerId&&w.IsActive!=false)
                .Include(i => i.Product)
                .Include(w => w.Motorcycle).OrderByDescending(x=>x.CreatedAt)
                .ToListAsync();

            if (wishList == null)
                return new ServiceResponse { Success = false, Message = "WishList not found." };

            return new ServiceResponse { Success = true, Data = wishList };
        }

    
     
    
        public async Task<ServiceResponse> DeleteWishList(int wishListId)
        {
            var wishList = await _context.WishLists.FindAsync(wishListId);
            if (wishList == null)
                return new ServiceResponse { Success = false, Message = "WishList not found." };
            wishList.IsActive = false;
            _context.WishLists.Update(wishList);
            await _context.SaveChangesAsync();

            return new ServiceResponse { Success = true, Message = "WishList deleted." };
        }
    }
}
    