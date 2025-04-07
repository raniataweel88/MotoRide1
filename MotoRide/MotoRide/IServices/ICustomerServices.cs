using INTEGRATEDAPI.Shared;

namespace MotoRide.IServices
{
    public interface ICustomerServices
    {
        public Task<ServiceResponse> AddPoints(int userId, int points);
        public Task<ServiceResponse> GetLevel(int userId);
    }
}
