using INTEGRATEDAPI.Shared;
using MotoRide.Dto;

namespace MotoRide.IServices
{
    public interface IMotocyleService
    {
        public Task<ServiceResponse> GetMotorcycle(int motorcycleId);
        public Task<ServiceResponse> GetAllUsedMotorcycle(int storeId);
        public Task<ServiceResponse> GetAllNewMotorcycle(int storeId);
        public  Task<ServiceResponse> GetAllMotorcycle(int storeId);
        public Task<ServiceResponse> GetAllMotorcycle();
        public Task<ServiceResponse> GetMotorcycleByShop(int shopId);
        public Task<ServiceResponse> AddMotorcycle(AddMotorcycleDto dto, IFormFile? images);
        public Task<ServiceResponse> UpdateMotorcycle(UpdateMotorcycleDto dto, IFormFile? images);
        public Task<ServiceResponse> DeleteMotorcycle(int motorcycleId);
    }
}
