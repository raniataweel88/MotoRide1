using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MotoRide.Services
{
    public class ProductSerives : IProductSerives
    {
      private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _response;
        private readonly ImageServices _imageServices;
        public ProductSerives(MotoRideDbContext context, ServiceResponse response, ImageServices imageServices)
        {
            _context = context;
            _response = response;
            _imageServices = imageServices;
        }
        public async Task<ServiceResponse> GetAllProduct(int storeId)
        {
            try
            {
                var product = from p in await _context.Products
                              .Where(x => x.IsActive != false&&x.StoreId==storeId)
                              .ToListAsync()
                              select new CardProductDto
                              {
                                  ProductId = p.ProductId,
                                  Name = p.Name,
                                  Images = $"http://localhost:5147{p.Images}",
                                  Price = p.Price,
                              };
                if (product == null)
                {
                    _response.Message = "can not get all product";
                    _response.Success = false;
                }
                _response.Data = product;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not get all product" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetAllProduct()
        {
            try
            {
                var product = from p in await _context.Products
                              .Where(x=>x.IsActive!= false)
                              .ToListAsync()
                              select new CardProductDto
                              {
                                  ProductId = p.ProductId,
                                  Name = p.Name,
                                  Images = $"http://localhost:5147{p.Images}",
                                  Price = p.Price,
                              };
                if (product == null)
                {
                    _response.Message = "can not get all product";
                    _response.Success= false;
                }
                _response.Data = product;
                _response.Success = true;

            } 
            catch(Exception e)
            {
                _response.Message = "can not get all product"+e.Message;
                _response.Success = false;
            }    
            return _response;
        }
        public async Task<ServiceResponse> GetProduct(int productId)
        {
            try
            {
                var product =await _context.Products.FirstOrDefaultAsync(x=>x.ProductId == productId);
                if (product == null)
                {
                    _response.Message = $"can not get this {productId} product";
                    _response.Success = false;
                }
                var productjson = new
                {
                    Colors = product.Colors,
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Sizes = product.Sizes,
                    Images = $"http://localhost:5147{product.Images}",
                    Price = product.Price,
                    ShopOwnerId = product.StoreId,
                    CategoryId = product.CategoryId,
                    SubCategoryId = product.SubCategoryId,
                    Quantity = product.Quantity,
                };

                _response.Data = productjson;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {productId} product" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetProductbyCategory(int categoryId)
        {
            try
            {
                var products = from p in _context.Products.Where(x => x.SubCategoryId == categoryId && x.IsActive != false)
                                              select new CardProductDto
                                              {
                                                  ProductId = p.ProductId,
                                                  Name = p.Name,
                                                  Images = $"http://localhost:5147{p.Images}",
                                                  Price = p.Price,
                                              };
                if (products == null)
                {
                    _response.Message = $"can not get  product in this category{categoryId}";
                    _response.Success = false;
                }
                _response.Data = products;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get product in this category{categoryId}" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> AddProduct(AddProductDto dto, IFormFile? productImageFile)
        {
            try
            {
                string? productImagePath = null;

                // Check if a product image file is provided
                if (productImageFile != null)
                {
                    // Save the image and get its path
                    productImagePath = await _imageServices.Imges(productImageFile);
                }

                Product p = new Product()
                {
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    Sizes = dto.Sizes,
                    SubCategoryId = dto.SubCategoryId,
                    Colors = dto.Colors,
                    Name = dto.Name,
                    RemainingQuantity = dto.Quantity,
                    CategoryId = 1,
                    Images = productImagePath,
                    Description = dto.Description,
                    StoreId = dto.StoreId,
                    CreatedAt = DateTime.Now,
                };
                await _context.Products.AddAsync(p);
                await _context.SaveChangesAsync();
                _response.Message = "done to add new product";
                _response.Success = true;
            }
            catch (Exception e)
            {
                _response.Message = "can not add new product" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> UpdateProduct(UpdateProductDto dto, IFormFile? productImageFile)
        {
            try
            {
                string? productImagePath = null;

                // Check if a product image file is provided
                if (productImageFile != null)
                {
                    // Save the image and get its path
                    productImagePath = await _imageServices.Imges(productImageFile);
                }

                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == dto.ProductId);
                if (product == null)
                {
                    _response.Message = $"can not get this {dto.ProductId} product";
                    _response.Success = false;
                }
                else { 
                product.Price = dto.Price;
                product.Quantity = dto.Quantity;
                product.Sizes = dto.Sizes;
                product.Colors = dto.Colors;
                product.Name = dto.Name;
                product.ProductId = dto.ProductId;
                product.CategoryId = 1;
                  if(productImagePath!=null)
                product.Images = productImagePath;
                    product.RemainingQuantity = dto.Quantity;

                product.Description = dto.Description;
             
                 _context.Products.Update(product);
                await _context.SaveChangesAsync();
                _response.Message = $"done to update this {dto.ProductId} product";
                _response.Success = true;
                }

            }
            catch (Exception e)
            {
                _response.Message = $"can not update this {dto.ProductId} product" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> DeleteProduct(int productId)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product == null)
                {
                    _response.Message = $"can not delete this {productId} product";

                    _response.Success = false;
                    return _response;

                }
                product.IsActive = false;
                _context.Update(product);
                await _context.SaveChangesAsync();
                _response.Success = true;
                _response.Message = $"  delete this {productId} product succefully";
           
            }
            catch (Exception e)
            {
                _response.Message = $"can not delete this {productId} product" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

        public async Task<ServiceResponse> GetProductByShop(int shopId)
        {
            try
            {
                var products =  from p in 
                  _context.Products.Where(x => x.StoreId == shopId&&x.IsActive!= false)
                   select new CardProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                       Images = $"http://localhost:5147{p.Images}",
                       Price = p.Price,
                    Quantity = p.Quantity,
                       RemainingQuantity= p.RemainingQuantity
                }; ;
                if (products == null)
                {
                    _response.Message = $"can not get  product in  this {shopId} shop ";
                    _response.Success = false;
                }
                _response.Data = products;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get  product in  this {shopId} shop " + e.Message;
                _response.Success = false;
            }
            return _response;
        }
    }
}
