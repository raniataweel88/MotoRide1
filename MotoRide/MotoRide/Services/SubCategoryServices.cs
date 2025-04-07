using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using static MotoRide.Services.SubCategoryServices;

namespace MotoRide.Services
{
    public class SubCategoryServices: ISubCategoryServices
        {
            private readonly MotoRideDbContext _context;
            private readonly ServiceResponse _response;
            public SubCategoryServices(MotoRideDbContext context, ServiceResponse response)
            {
                _context = context;
                _response = response;
            }
            public async Task<ServiceResponse> GetAllSubCategory()
            {
                try
                {
                    var subCategory = from c in await _context.SubCategories
                                      .Where(x=>x.IsActive!=false).ToListAsync()
                                      select new
                                      {
                                          Name=c.Name,
                                          SubCategoryId= c.SubCategoryId,
                                          CreatedAt=c.CreatedAt
                                      };
                if (subCategory == null )
                {
                    _response.Message = "Cannot get subcategories. No data found.";
                    _response.Success = false;
                }
                else
                {
                    _response.Data = subCategory;
                    _response.Success = true;
                    _response.Message = "Successfully retrieved subcategories.";
                }
            
            }
                catch (Exception e)
                {
                    _response.Message = $"can not get  Sub Category" + e.Message;
                    _response.Success = false;
                }
                return _response;
            }

        public async Task<ServiceResponse> GetAllSubCategoryByShop(int id)
        {
            try
            {
                var subCategory = await _context.SubCategories.Where(x =>x.ShopId==id&& x.IsActive != false).ToListAsync();
                if (subCategory == null)
                {
                    _response.Message = $"can not get subCategory";
                    _response.Success = false;
                }
                _response.Data = subCategory;
                _response.Success = true;
                _response.Message = $"done to get subCategory";

            }
            catch (Exception e)
            {
                _response.Message = $"can not get  Sub Category" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetSubCategory(int subCategoryId)
            {
                try
                {
                    var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryId == subCategoryId);
                    if (subCategory == null)
                    {
                        _response.Message = $"can not get this {subCategoryId} subCategory";
                        _response.Success = false;
                    }
                    _response.Data = subCategory;
                    _response.Success = true;

                }
                catch (Exception e)
                {
                    _response.Message = $"can not get this {subCategoryId} subCategory" + e.Message;
                    _response.Success = false;
                }
                return _response;
            }
            public async Task<ServiceResponse> AddSubCategory(SubCategoryDto dto)
            {
                try
                {
                SubCategory c = new SubCategory
                {
                    Name = dto.Name,
                    ShopId=dto.ShopId,
                    CreatedAt = DateTime.Now,

                    };

                    await _context.SubCategories.AddAsync(c);
                    await _context.SaveChangesAsync();
                    _response.Message = "done to add new Sub Category";
                    _response.Success = true;
                }
                catch (Exception e)
                {
                    _response.Message = $"can not add new Sub Category" + e.Message;
                    _response.Success = false;
                }
                return _response;
            }
            public async Task<ServiceResponse> UpdateSubCategory(UpdateSubCategoryDto dto)
            {
                try
                {
                    var Category = await _context.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryId == dto.SubCategoryId);
                    if (Category == null)
                    {
                        _response.Message = $"can not get this {dto.SubCategoryId} SubCategory";
                        _response.Success = false;
                    }
                    Category.Name = dto.Name;
                dto.ShopId = dto.ShopId;
                _context.Update(Category);
                    await _context.SaveChangesAsync();
                    _response.Message = $"done to update this {dto.SubCategoryId} SubCategory";
                    _response.Success = true;

                }
                catch (Exception e)
                {
                    _response.Message = $"can not update this {dto.SubCategoryId} SubCategory" + e.Message;
                    _response.Success = false;
                }
                return _response;
            }
            public async Task<ServiceResponse> DeleteSubCategory(int SubCategoryId)
            {
                try
                {
                    var Categories = await _context.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryId == SubCategoryId);
                    if (Categories != null)
                    {
                        Categories.IsActive = false;
                        _context.SubCategories.Update(Categories);
                        await _context.SaveChangesAsync();
                        _response.Success = true;
                    }
                else { 
                    _response.Message = $"can not delete this {SubCategoryId} SubCategory";
                    _response.Success = false;
                }}
                catch (Exception e)
                {
                    _response.Message = $"can not delete this {SubCategoryId} SubCategory" + e.Message;
                    _response.Success = false;
                }
                return _response;
            }
        }
    } 
