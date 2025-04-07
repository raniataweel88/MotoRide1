using INTEGRATEDAPI.Shared;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using Microsoft.EntityFrameworkCore;
using Azure;
using MotoRide.Migrations;
using Azure.Core;

namespace MotoRide.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly MotoRideDbContext _context;

        public ReviewServices(MotoRideDbContext dbContext)
        {
            _context = dbContext;
        }

        // ✅ Add a new review
        public async Task<ServiceResponse> AddReview(AddReviewDto dto)
        {
            var response = new ServiceResponse();

            try
            {
                Review review = new Review();
                review.Comment = dto.Comment;
                review.Rating = dto.Rating;
                review.CustomerId = dto.CustomerId;
                if (dto.MotorcycleId != 0)
                {
                    review.MotorcycleId = dto.MotorcycleId;

                }
                if (dto.ProductId != 0)
                {
                    review.ProductId = dto.ProductId;

                }
                review.CreatedAt = dto.CreatedAt ?? DateTime.UtcNow;
                review.IsActive = true;
                review.StoreId = dto.StoreId;

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Review added successfully!";
                response.Data = review;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ✅ Get a review by ID
        public async Task<ServiceResponse> GetReview(int reviewId)
        {
            var response = new ServiceResponse();

            try
            {
                var review = await _context.Reviews
                    .Include(r => r.CustomerId)
                    .Include(r => r.Product)
                    .Include(r => r.Motorcycle)
                    .FirstOrDefaultAsync(r => r.ReviewId == reviewId);

                if (review == null)
                {
                    response.Success = false;
                    response.Message = "Review not found.";
                    return response;
                }

                response.Success = true;
                response.Message = "Review retrieved successfully.";
                response.Data = review;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ✅ Get all reviews
        public async Task<ServiceResponse> GetAllReviewForThisItem(int? ProductId, int? MotorcycleId)
        {
            var response = new ServiceResponse();

            try
            {
                var reviews = await _context.Reviews 
                    .Include(r => r.Product)
                    .Include(r => r.Motorcycle)
                    .Include(r=>r.Customer)
                    .Where(r => r.IsActive != false&&(r.ProductId== ProductId || r.MotorcycleId== MotorcycleId))
                    .ToListAsync();
            

                response.Success = true;
                response.Message = "Reviews retrieved successfully.";
                response.Data = reviews;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ✅ Update a review
        public async Task<ServiceResponse> UpdateReview(UpdateReviewDto dto)
        {
            var response = new ServiceResponse();

            try
            {
                var review = await _context.Reviews.FindAsync(dto.ReviewId);

                if (review == null)
                {
                    response.Success = false;
                    response.Message = "Review not found.";
                    return response;
                }

                review.Comment = dto.Comment ?? review.Comment;
                review.Rating = dto.Rating ?? review.Rating;
                review.CustomerId = dto.CustomerId ?? review.CustomerId;
                if (dto.MotorcycleId != 0)
                {
                    review.MotorcycleId = dto.MotorcycleId;

                }
                if (dto.ProductId != 0)
                {
                    review.ProductId = dto.ProductId;

                }
                review.StoreId = dto.StoreId;

                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Review updated successfully!";
                response.Data = review;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ✅ Delete a review (Soft Delete)
        public async Task<ServiceResponse> DeleteReview(int reviewId)
        {
            var response = new ServiceResponse();

            try
            {
                var review = await _context.Reviews.FindAsync(reviewId);

                if (review == null)
                {
                    response.Success = false;
                    response.Message = "Review not found.";
                    return response;
                }

                review.IsActive = false; // Soft delete (instead of removing from DB)
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Review deleted successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // ✅ Get all reviews for a specific shop
        public async Task<ServiceResponse> GetReviewByShop(int shopId,int? motorcycleId,int? productId)
        {
            var response = new ServiceResponse();

            try
            {
                var reviews = await _context.Reviews
                    .Include(r => r.Product)
                    .Include(r => r.Motorcycle)
                    
                    .Where(r => r.StoreId== shopId && (r.MotorcycleId == motorcycleId|| r.ProductId == productId) )// Assuming shopId maps to product/motorcycle
                    .ToListAsync();

                if (reviews.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No reviews found for this shop.";
                    return response;
                }

                response.Success = true;
                response.Message = "Reviews retrieved successfully.";
                response.Data = reviews;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteReviewByStore(DeleteRviewByStoreDto dto) { 
           var response = new ServiceResponse();

            try
            {
                var review = await _context.Reviews
        .Where(r => r.ReviewId == dto.ReviewId )// Assuming shopId maps to product/motorcycle
                    .FirstOrDefaultAsync();

                if (review!=null)
                {
                    review.StoreNeedDeletedReview = dto.StoreNeedDeletedReview;
                    review.StoreReason = dto.StoreReason;
                    _context.Reviews.Update(review);
                    await _context.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Reviews retrieved successfully.";
                    response.Data = review;
                }
                else{
                    response.Success = false;
                    response.Message = "No reviews found for this shop.";
                    return response;

                }

   
            }
            catch (Exception ex)
            {
                response.Success = false;
response.Message = ex.Message;
            }

            return response;
        }
        public async Task<ServiceResponse> DeleteReviewByAdmain(DeleteRviewByAdmainDto dto)
        {
            var response = new ServiceResponse();

            try
            {
                var review = await _context.Reviews
        .Where(r => r.ReviewId == dto.ReviewId)// Assuming shopId maps to product/motorcycle
                    .FirstOrDefaultAsync();

                if (review != null)
                {
                    review.AdminNeedDeletedReview = dto.AdminNeedDeletedReview;
                    review.AdminReason = dto.AdminReason;
                    if (dto.AdminNeedDeletedReview == true)
                    {

                        review.IsActive = false; // Soft delete (instead of removing from DB)
                      
                    }
                    _context.Reviews.Update(review);
                    await _context.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Reviews retrieved successfully.";
                    response.Data = review;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No reviews found for this shop.";
                    return response;

                }


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> GetDeleteReview(GetDeleteStoreDto dto)
        {
            var response = new ServiceResponse();

            try
            {
                var review = await _context.Reviews
        .Where(r => r.ReviewId == dto.ReviewId && r.StoreId == dto.StoreId && (r.MotorcycleId == dto.MotorcycleId || r.ProductId == dto.ProductId))// Assuming shopId maps to product/motorcycle
             .Select(r => new GetDeleteDto
             {
                 Rating = r.Rating,
                 ReviewId=r.ReviewId,
                 Comment=r.Comment,
                 StoreId = r.StoreId,
                 AdminReason = r.AdminReason,
                 AdminNeedDeletedReview = r.AdminNeedDeletedReview,
                 CustomerId = r.CustomerId,
                 MotorcycleId = r.MotorcycleId,
                 ProductId = r.ProductId,
                  StoreNeedDeletedReview = r.StoreNeedDeletedReview,
                  StoreReason = r.StoreReason,
                  IsActive= r.IsActive,

             })
                    .FirstOrDefaultAsync();
               
                if (review != null)
                {
                    
                    response.Success = true;
                    response.Message = "Reviews retrieved successfully.";
                    response.Data = review;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No reviews found for this shop.";
                    return response;

                }


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}

