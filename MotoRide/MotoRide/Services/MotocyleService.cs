using Azure;
using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Services
{
    public class MotocyleService: IMotocyleService
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _response;
        private readonly ImageServices _imageServices;
        public MotocyleService(MotoRideDbContext context, ServiceResponse response, ImageServices imageServices)
        {
            _context = context;
            _response = response;
            _imageServices = imageServices;
        }
        public async Task<ServiceResponse> GetAllUsedMotorcycle(int storeId)  
        {
            try
            {
                var motorcycle = from p in await _context.Motorcycles
                                          .Where(x =>  x.IsActive != false&&x.Condition=="Used"&&x.StoreId== storeId)
                                          .ToListAsync()
                                 select new CardMotorcycleDto
                                 {
                                     MotorcycleId = p.MotorcycleId,
                                     Name = p.Name,
                                     Images = $"http://localhost:5147{p.Images}",
                                     Price = p.Price,
                                     CategoryId = p.CategoryId,
                                     StoreId = p.StoreId
                                 };

                _response.Data = motorcycle;
                    _response.Success = true;
                
            }

            catch (Exception e)
            {
                _response.Message = "can not get all motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetAllMotorcycle(int storeId)
        {
            try
            {
                var motorcycle = from p in await _context.Motorcycles
                                          .Where(x => x.IsActive != false && x.StoreId == storeId)
                                          .ToListAsync()
                                 select new CardMotorcycleDto
                                 {
                                     MotorcycleId = p.MotorcycleId,
                                     Name = p.Name,
                                     Images = $"http://localhost:5147{p.Images}",
                                     Price = p.Price,
                                     CategoryId = p.CategoryId,
                                     StoreId = p.StoreId
                                 };

                _response.Data = motorcycle;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not get all motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
                 public async Task<ServiceResponse> GetAllMotorcycle()
            {
                try
                {
                    var motorcycle = from p in await _context.Motorcycles
                                              .Where(x => x.IsActive != false )
                                              .ToListAsync()
                                     select new CardMotorcycleDto
                                     {
                                         MotorcycleId = p.MotorcycleId,
                                         Name = p.Name,
                                         Images = $"http://localhost:5147{p.Images}",
                                         Price = p.Price,
                                         CategoryId = p.CategoryId,
                                         StoreId = p.StoreId
                                     };

                    _response.Data = motorcycle;
                    _response.Success = true;

                }

                catch (Exception e)
            {
                _response.Message = "can not get all motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetAllNewMotorcycle(int storeId)
        {
            try
            {
                var motorcycle = from p in await _context.Motorcycles
                                          .Where(x => x.IsActive != false && x.Condition == "New"&& x.StoreId== storeId)
                                          .ToListAsync()
                                 select new CardMotorcycleDto
                                 {
                                     MotorcycleId = p.MotorcycleId,
                                     Name = p.Name,
                                     Images = $"http://localhost:5147{p.Images}",
                                     Price = p.Price,
                                     CategoryId = p.CategoryId,
                                     StoreId = p.StoreId,
                                     
                                 };

                _response.Data = motorcycle;
                _response.Success = true;

            }

            catch (Exception e)
            {
                _response.Message = "can not get all motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetMotorcycle(int motorcycleId)
        {
            try
            {
                var motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(x => x.MotorcycleId == motorcycleId);
                if (motorcycle == null)
                {
                    _response.Message = $"can not get this {motorcycleId} motorcycle";
                    _response.Success = false;
                }
                var motorcycleJson = new
                {
                    Colors = motorcycle.Colors,
                    MotorcycleId = motorcycle.MotorcycleId,
                    Name = motorcycle.Name,
                    Description = motorcycle.Description,
                    Brand = motorcycle.Brand,
                    Images = $"http://localhost:5147{motorcycle.Images}",
                    Price = motorcycle.Price,
                    ShopOwnerId = motorcycle.StoreId,
                    CategoryId = motorcycle.CategoryId,
                    Mileage = motorcycle.Mileage,
                    Quantity = motorcycle.StockQuantity,
                    Condition = motorcycle.Condition,
                    Year = motorcycle.Year,
                    EngineType = motorcycle.EngineType,
                    motorcycle.StockQuantity,

                };

                _response.Data = motorcycleJson;
                _response.Success = true;
                return _response;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {motorcycleId} motorcycle" + e.Message;
                _response.Success = false;
                return _response;
            }
           
        }
        public async Task<ServiceResponse> GetMotorcycleByShop(int shopId)
        {
            try
            {
                var motorcycle = from p in await _context.Motorcycles
                                           .Where(x =>x.StoreId == shopId&& x.IsActive != false)
                                           .ToListAsync()
                                 select new CardMotorcycleDto
                                 {
                                     MotorcycleId = p.MotorcycleId,
                                     Name = p.Name,
                                     Images = $"http://localhost:5147{p.Images}",
                                     Price = p.Price,
                                     Quantity = p.StockQuantity,
                                     StoreId = p.StoreId,
                                     RemainingQuantity= p.RemainingQuantity,
                                 };
                if (motorcycle == null)
                {
                    _response.Message = $"can not get  motorcycles in  this {shopId} shop ";
                    _response.Success = false;
                }
                _response.Data = motorcycle;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get  motorcycles in  this {shopId} shop " + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> AddMotorcycle(AddMotorcycleDto dto, IFormFile? images)
        {
            try
            {
                string? productImagePath = null;

                // Check if a product image file is provided
                if (images != null)
                {
                    // Save the image and get its path
                    productImagePath = await _imageServices.Imges(images);
                }

                Motorcycle m = new Motorcycle()
                {
                    Price = dto.Price,
                    StockQuantity = dto.StockQuantity,
                    Colors = dto.Colors,
                    Name = dto.Name,
                    CategoryId = 2,
                    Images = productImagePath,
                    Mileage = dto.Mileage,
                    EngineType = dto.EngineType,
                    Year = dto.Year,
                    Brand = dto.Brand,
                    StoreId = dto.StoreId,
                    Condition = dto.Condition,
                    RemainingQuantity=dto.StockQuantity,
                    Description = dto.Description,
                    CreatedAt = DateTime.Now,
                };
                await _context.Motorcycles.AddAsync(m);
                await _context.SaveChangesAsync();
                _response.Message = "done to add new Motorcycle";
                _response.Success = true;
            }
            catch (Exception e)
            {
                _response.Message = $"can not add new Motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> UpdateMotorcycle(UpdateMotorcycleDto dto, IFormFile? images)
        {
            try
            {
                string? productImagePath = null;

                // Check if a product image file is provided
                if (images != null)
                {
                    // Save the image and get its path
                    productImagePath = await _imageServices.Imges(images);
                }

                var Motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(x => x.MotorcycleId == dto.MotorcycleId);
                if (Motorcycle == null)
                {
                    _response.Message = $"can not get this {dto.MotorcycleId} Motorcycles";
                    _response.Success = false;
                }

                Motorcycle.Price = dto.Price;
                Motorcycle.StockQuantity = dto.StockQuantity;
                Motorcycle.RemainingQuantity = dto.StockQuantity;
                Motorcycle.Colors = dto.Colors;
                Motorcycle.Name = dto.Name;
                Motorcycle.CategoryId = 2;
                if(images!=null)
                Motorcycle.Images = productImagePath;
               Motorcycle.Mileage = dto.Mileage;
                Motorcycle.EngineType = dto.EngineType;
                Motorcycle.Year = dto.Year;
                Motorcycle.Brand = dto.Brand;
                Motorcycle.StoreId = dto.StoreId;
                Motorcycle.Condition = dto.Condition;
                Motorcycle.Description = dto.Description;
                
                _context.Update(Motorcycle);
                await _context.SaveChangesAsync();
                _response.Message = $"done to update this {dto.MotorcycleId} Motorcycle";
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not update this {dto.MotorcycleId} Motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> DeleteMotorcycle(int motorcycleId)
        {
            try
            {
                var motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(x => x.MotorcycleId == motorcycleId);
                if (motorcycle == null)
                {
                    _response.Message = $"can not delete this {motorcycleId} motorcycle";
                    _response.Success = false;
                }
           
                motorcycle.IsActive = false;
                    _context.Update(motorcycle);
                    await _context.SaveChangesAsync();
                   _response.Success = true;
                _response.Message = $" done to delete this {motorcycleId} motorcycle ";
            }
            catch (Exception e)
            {
                _response.Message = $"can not delete this {motorcycleId} motorcycle" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
    }
}
