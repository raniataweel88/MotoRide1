using INTEGRATEDAPI.Shared;
using MotoRide.Dto;
using MotoRide.Models;

namespace MotoRide.IServices
{
    public interface ICatrgoiresServices
    {
        public Task<ServiceResponse> GetCatrgoires(int CatrgoiresId);
        public Task<ServiceResponse> GetAllCatrgoires();
        public Task<ServiceResponse> AddCatrgoires(string name);
        public Task<ServiceResponse> UpdateCatrgoires(CategoryDto c);
        public Task<ServiceResponse> DeleteCatrgoires(int catrgoiresId);
    }
}
