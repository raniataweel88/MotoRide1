using Azure;
using INTEGRATEDAPI.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using System.Drawing;

namespace MotoRide.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _response;
        private readonly ICustomerServices _customer;
        public OrderServices(MotoRideDbContext context, ServiceResponse response,ICustomerServices customer)
        {
            _context = context;
            _response = response;
            _customer = customer;   
        }
        public async Task<ServiceResponse> GetAllOrder()
        {
            try
            {
                var order =  await _context.Orders
                              .Where(x => x.IsActive != true)
                              .ToListAsync();
                       
                if (order == null)
                {
                    _response.Message = "can not get all order";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not get all order" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetAllOrderReceivedByShop(int shopId)
        {
            try
            {
                var order = await _context.OrderItems
      .Include(x => x.Order)
      .Where(x => x.StoreId == shopId
               && x.Order.IsActive != true
               && x.Order.StatusDelivery == true)
      .GroupBy(x => new
      {
          x.OrderId,
          x.Order.Title,
          x.Order.StatusPayment,
          x.Order.RecivingDate

      })
      .Select(group => new
      {
          OrderId = group.Key.OrderId,
          Title = group.Key.Title,
          TotalPrice = group.Sum(item => item.Price * item.Quantity),
          StatusPayment = group.Key.StatusPayment,
          RecivingDate = group.Key.RecivingDate,
      })
      .ToListAsync();


                if (order == null)
                {
                    _response.Message = "can not Get All Order Not Received By Shop";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not Get All Order Not Received By Shop" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetAllOrderNotReceivedByShop(int shopId)
        {
            try
            {
                var order= await _context.OrderItems
      .Include(x => x.Order)
      .Where(x => x.StoreId == shopId
               && x.Order.IsActive != true
               && x.Order.StatusDelivery != true)
      .GroupBy(x => new
      {
          x.OrderId,
          x.Order.Title,
          x.Order.StatusPayment,
          x.Order.RecivingDate

      })
      .Select(group => new
      {
          OrderId = group.Key.OrderId,
          Title = group.Key.Title,
          TotalPrice = group.Sum(item => item.Price * item.Quantity),
          StatusPayment=  group.Key.StatusPayment,
          RecivingDate = group.Key.RecivingDate,
      })
      .ToListAsync();


                if (order == null)
                {
                    _response.Message = "can not Get All Order Not Received By Shop";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not Get All Order Not Received By Shop" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetItemOrderReceivedByShop(int shopId, int orderId)
        {
            try
            {
                var order = await _context.OrderItems
                                .Where(x => x.StoreId == shopId && x.OrderId == orderId && x.Order.IsActive != true && x.Order.StatusDelivery == true)
                              .ToListAsync();

                if (order == null)
                {
                    _response.Message = "can not Get All item in Order  Received By Shop";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not All item in Order  Received By Shop" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

        public async Task<ServiceResponse> GetItemOrderNotReceivedByShop(int shopId, int orderId)
        {
            try
            {
                var order = await _context.OrderItems
                                .Where(x => x.StoreId == shopId && x.OrderId == orderId && x.Order.IsActive != true && x.Order.StatusDelivery != true)
                              .ToListAsync();

                if (order == null)
                {
                    _response.Message = "can not Get All Order Not Received By Shop";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = "can not Get All Order Not Received By Shop" + e.Message;
                _response.Success = false;
            }
            return _response;
        }

        public async Task<ServiceResponse> GetOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                if (order == null)
                {
                    _response.Message = $"can not get this {orderId} order";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {orderId} order" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetOrderDetails(int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                var orderItems = await _context.OrderItems.Where(x => x.OrderId == orderId).Include(x=>x.Product).Include(x=>x.Motorcycle).ToListAsync();

                if (order == null)
                {
                    _response.Message = $"can not get this {orderId} order";
                    _response.Success = false;
                }
                _response.Data = new { order, orderItems };
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {orderId} order" + e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetAllOrderforUser(int customerId)
        {
            try
            {
                var order = await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
                if (order == null)
                {
                    _response.Message = $"can not get orders for this {customerId}customer ";
                    _response.Success = false;
                }
                _response.Data = order;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get orders for this {customerId}customer ";
                _response.Success = false;
            }
            return _response;
        }

        public async Task<ServiceResponse> AddOrder(AddOrderDto dto)
        {
            try
            {
                if (dto.CustomerId == null)
                {
                    _response.Message = "Customer ID is required.";
                    _response.Success = false;
                    return _response;
                }

                var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CustomerId == dto.CustomerId);
                if (cart == null)
                {
                    _response.Message = "Cart not found for this customer.";
                    _response.Success = false;
                    return _response;
                }

                var cartItems = await _context.CartItems
                    .Where(x => x.CartId == cart.CartId && x.IsActive != false)
                    .ToListAsync();

                if (cartItems == null || !cartItems.Any())
                {
                    _response.Message = "No active cart items found.";
                    _response.Success = false;
                    return _response;
                }

                // إنشاء الطلب
                Order o = new Order()
                {
                    Title = dto.Title,
                    CustomerId = dto.CustomerId,
                    TotalPrice = dto.TotalPrice,
                    RecivingDate = dto.RecivingDate,
                    IsActive = false,
                    CustomerNote = dto.CustomerNote,
                    CreatedAt = DateTime.Now,
                    StatusPayment = dto.StatusPayment,
                };
                await _context.Orders.AddAsync(o);
                await _context.SaveChangesAsync();

                foreach (var item in cartItems)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                    var motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(x => x.MotorcycleId == item.MotorcycleId);

                    // التحقق من أن المنتج أو الدراجة النارية موجودة قبل استخدامها
                    if (product == null && motorcycle == null)
                    {
                        _response.Message = "Product or Motorcycle not found.";
                        _response.Success = false;
                        return _response;
                    }

                    OrderItem orderItem = new OrderItem()
                    {
                        Color = item.Color,
                        MotorcycleId = item.MotorcycleId,
                        Size = item.Size,
                        OrderId=o.OrderId,
                        Name = product?.Name ?? motorcycle?.Name ?? "",
                        Quantity = (int)item.Quantity,
                        Image= product?.Images ?? motorcycle?.Images ?? "",
                        ProductId = item.ProductId,
                        Price = product?.Price ?? motorcycle?.Price ?? 0, // إذا كان أحدهما غير متوفر، استخدم الآخر
                        StoreId = product?.StoreId ?? motorcycle?.StoreId ?? 0,
                        CreatedAt = DateTime.Now
                    };

                  await  _context.OrderItems.AddAsync(orderItem); // ربط العنصر مباشرة بالطلب
                    item.IsActive = false;
                    _context.CartItems.Update(item);
                    await _context.SaveChangesAsync();
                }

               
               var user=await _context.Users.FirstOrDefaultAsync(x => x.UserId == dto.CustomerId);
                user.Phone = dto.Phone;
                user.Location = dto.Location;
                if (_customer != null)
                {
                    await _customer.AddPoints(dto.CustomerId ?? 0, 20);
                }

                _response.Message = "Order added successfully.";
                _response.Success = true;
            }
            catch (Exception e)
            {
                _response.Message = $"Cannot add new order: {e.Message}";
                _response.Success = false;
            }

            return _response;
        }


        public async Task<ServiceResponse> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var cart = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == dto.OrderId);
                if (cart == null)
                {
                    _response.Message = $"can not get this {dto.OrderId} order";
                    _response.Success = false;
                }
                Order order = new Order()
                {
                    Title = dto.Title,
                    CustomerId = dto.CustomerId,
                    TotalPrice = dto.TotalPrice,
                    RecivingDate = dto.RecivingDate,
                    IsActive = false,
                    StatusDelivery = dto.StatusDelivery,
                    CustomerNote = dto.CustomerNote,
                    CreatedAt = DateTime.Now,
                };
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _response.Message = "done to update order";
                _response.Success = true;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {dto.OrderId} order" + e.Message;
                _response.Success = false;
            }
            return _response;

        }
        public async Task<ServiceResponse> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                if (order == null)
                {
                    _response.Message = $"can not get this {orderId} order";
                    _response.Success = false;
                }
                if(order != null) {
                order.IsActive = true;
                _context.Orders.Update(order); 
                await _context.SaveChangesAsync();
                _response.Message = "done to delete order";
                _response.Success = true;
            }
            }
            catch (Exception e)
            {
                _response.Message = $"can not get this {orderId} order" + e.Message;
                _response.Success = false;
                return _response;
            }
            return _response;

        }
        public async Task<ServiceResponse> GetAllOrderForthisShop(int shopId)
        {
            try
            {
                var orders = await _context.OrderItems.Where(x => x.StoreId == shopId).ToListAsync();
                if (orders == null)
                {
                    _response.Message = $"can not get orders for this shop{shopId}";
                    _response.Success = false;
                }
                _response.Data = orders;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get orders for this shop{shopId}"+e.Message;
                _response.Success = false;
            }
            return _response;
        }
        public async Task<ServiceResponse> GetOrderByIdForthisShop(int orderId ,int StoreId)
        {
            try
            {
                var orders = await _context.OrderItems.
                    FirstOrDefaultAsync(x => x.OrderId == orderId&& x.StoreId == StoreId)
                    ;
                if (orders == null)
                {
                    _response.Message = $"can not get this order for this shop{StoreId}";
                    _response.Success = false;
                }
                _response.Data = orders;
                _response.Success = true;

            }
            catch (Exception e)
            {
                _response.Message = $"can not get this order for this shop{StoreId}"+e.Message;
                _response.Success = false;
            }
            return _response;
        }
    }
}
