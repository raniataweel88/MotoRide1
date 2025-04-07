using INTEGRATEDAPI.Shared;
using MotoRide.Dto;

namespace MotoRide.IServices
{
    public interface IReviewServices
    {
        public Task<ServiceResponse> GetReview(int reviewId);
        public Task<ServiceResponse> GetAllReviewForThisItem(int? ProductId, int? MotorcycleId);
        public Task<ServiceResponse> AddReview (AddReviewDto dto);
        public Task<ServiceResponse> UpdateReview(UpdateReviewDto dto);
        public Task<ServiceResponse> DeleteReview(int reviewId);
        public Task<ServiceResponse> GetReviewByShop(int shopId, int? motorcycleId, int? productId);
        public Task<ServiceResponse> DeleteReviewByStore(DeleteRviewByStoreDto dto);

        public Task<ServiceResponse> DeleteReviewByAdmain(DeleteRviewByAdmainDto dto);
        public Task<ServiceResponse> GetDeleteReview(GetDeleteStoreDto dto);

    }
}
