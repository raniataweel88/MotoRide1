using INTEGRATEDAPI.Shared;
using MotoRide.Dto;
using MotoRide.Models;

namespace MotoRide.IServices
{
    public interface ISubCategoryServices
    {
        public Task<ServiceResponse> GetSubCategory(int SubCategoryId);
        public Task<ServiceResponse> GetAllSubCategory();
        public Task<ServiceResponse> AddSubCategory(SubCategoryDto dto);
        public Task<ServiceResponse> UpdateSubCategory(UpdateSubCategoryDto dto);
        public Task<ServiceResponse> DeleteSubCategory(int SubCategoryId);
        public  Task<ServiceResponse> GetAllSubCategoryByShop(int id);
    }
}