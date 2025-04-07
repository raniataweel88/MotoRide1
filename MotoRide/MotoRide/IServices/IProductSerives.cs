using INTEGRATEDAPI.Shared;
using MotoRide.Dto;

namespace MotoRide.IServices
{
    public interface IProductSerives
    {
        public Task<ServiceResponse> GetProduct(int productId);
        public Task<ServiceResponse> GetAllProduct();
        public  Task<ServiceResponse> GetAllProduct(int storeId);
        public Task<ServiceResponse> GetProductByShop(int shopId);
        public Task<ServiceResponse> AddProduct(AddProductDto dto, IFormFile? productImageFile);
        public  Task<ServiceResponse> GetProductbyCategory(int categoryId);
        public Task<ServiceResponse> UpdateProduct(UpdateProductDto dto, IFormFile? images);
        public Task<ServiceResponse> DeleteProduct( int productId);
    }
}
