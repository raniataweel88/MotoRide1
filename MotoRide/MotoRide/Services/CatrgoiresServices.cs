using Azure;
using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Services
{
    public class CatrgoiresServices : ICatrgoiresServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _response;
        public CatrgoiresServices(MotoRideDbContext context, ServiceResponse response)
        {
            _context = context;
            _response = response;
        }
        public async Task<ServiceResponse> GetAllCatrgoires()
        {
            try
            {
                var Categories = await _context.Categories.Where(x=>x.IsActive!=true).ToListAsync();
                if (Categories == null)
                {
                    _response.Message = $"can not get Categories";
                    _response.Success = false;
                }
                _response.Data = Categories;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get  Categories" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

        public  async Task<ServiceResponse> GetCatrgoires(int CategoryId)
        {
            try
            {
                var Category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == CategoryId);
                if (Category == null)
                {
                    _response.Message = $"can not get this {CategoryId} Category";
                    _response.Success = false;
                }
                _response.Data = Category;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {CategoryId} Category" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

        public async Task<ServiceResponse> AddCatrgoires(string name)
        {
            try
            {
                Category c = new Category
                {
                    Name = name,
                    CreatedAt = DateTime.Now,
                   
                };

                await _context.Categories.AddAsync(c);
                await _context.SaveChangesAsync();
                _response.Message = "done to add new Category";
                _response.Success = true;
            }
            catch (Exception e)
            {
                _response.Message = $"can not add new Category" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

        public async Task<ServiceResponse> UpdateCatrgoires(CategoryDto dto) {
            try
            {
                var Category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == dto.CategoryId);
                if (Category == null)
                {
                    _response.Message = $"can not get this {dto.CategoryId} Category";
                    _response.Success = false;
                }
                Category.Name = dto.Name;
                _context.Update(Category);
                await _context.SaveChangesAsync();
                _response.Message = $"done to update this {dto.CategoryId} Category";
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not update this {dto.CategoryId} Category" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> DeleteCatrgoires(int CatrgoiresId)
        {
            try
            {
                var Categories = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == CatrgoiresId);
                if (Categories == null)
                {
                
                _response.Message = $"can not delete this {CatrgoiresId} Category";
                _response.Success = false;
        }
                Categories.IsActive = false;
                _response.Message = $"done to delete this {CatrgoiresId} Category";
                _context.Update(Categories);
                await _context.SaveChangesAsync();
                _response.Success = true;
            }
            catch (Exception e)
            {
                _response.Message = $"can not delete this {CatrgoiresId} Category" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

    }
}
