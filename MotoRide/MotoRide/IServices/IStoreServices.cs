using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace MotoRide.IServices
{
    public interface IStoreServices
    {
        public Task<ServiceResponse> GetAllStore();
        public  Task<ServiceResponse> AllowStoreToLogin(int id, bool IsCanLogin);
        public Task<ServiceResponse> GetAllStoreAccept();
        public Task<ServiceResponse> GetAllStoreReject();

        public Task<ServiceResponse> GetAllStoreNotResponse();
        }
}
