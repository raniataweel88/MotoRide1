using INTEGRATEDAPI.Shared;

namespace MotoRide.IServices
{
    public interface IStoreServices
    {
        public Task<ServiceResponse> GetAllStore();
        public  Task<ServiceResponse> AllowStoreToLogin(int id, bool IsCanLogin);
       
        }
}
