using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Migrations;
using MotoRide.Services;

namespace MotoRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAuthenticationServices _authentication;
        private readonly IProductSerives _serives;
        private readonly IMotocyleService _motocyleService;
        private readonly ICartServices _cart;
        private readonly ICartItemServices _cartItem;
        private readonly IOrderServices _order;
        private readonly IWishListServices _wishList;
        private readonly IReviewServices _reviewServices;
        public CustomerController(IAuthenticationServices authentication, IProductSerives serives,IOrderServices order,ICartItemServices cartItem,ICartServices cart,IWishListServices wishList,IMotocyleService motocyle 
            ,IReviewServices reviewServices)
        {
            _authentication = authentication;
            _serives = serives;
            _cart = cart;
            _cartItem = cartItem;
            _order = order;
            _wishList = wishList;
            _motocyleService = motocyle;
            _reviewServices = reviewServices;
        }
        
        #region Product
        [HttpGet("GetProductbyCategory/${id}")]
        public async Task<IActionResult> GetProductbyCategory(int id)
        {
            var response = await _serives.GetProductbyCategory(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _serives.GetAllProduct();
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllProduct/${storeId}")]
        public async Task<IActionResult> GetAllProduct(int storeId)
        {
            var response = await _serives.GetAllProduct(storeId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetProduct/${id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _serives.GetProduct(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region Motorcycle
        [HttpGet("GetMotorcycle/${id}")]
        public async Task<IActionResult> GetMotorcycle(int id)
        {
            var response = await _motocyleService.GetMotorcycle(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllUsedMotorcycle/${storeId}")]
        public async Task<IActionResult> GetAllUsedMotorcycle(int storeId)
        {
            var response = await _motocyleService.GetAllUsedMotorcycle(storeId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("GetAllMotorcycle/${storeId}")]
        public async Task<IActionResult> GetAllMotorcycle(int storeId)
        {
            var response = await _motocyleService.GetAllMotorcycle(storeId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("GetAllMotorcycle")]
        public async Task<IActionResult> GetAllMotorcycle()
        {
            var response = await _motocyleService.GetAllMotorcycle();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("GetAllNewMotorcycle/${storeId}")]
        public async Task<IActionResult> GetAllNewMotorcycle(int storeId)
        {
            var response = await _motocyleService.GetAllNewMotorcycle(storeId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion
        #region Cart
        [HttpGet("GetCart/${id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            var response = await _cart.GetCart(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetCartByCustomerId/${id}")]
        public async Task<IActionResult> GetCartByCustomerId(int id)
        {
            var response = await _cart.GetCartByCustomerId(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart([FromBody] AddCartDto dto)
        {
            var response = await _cart.AddCart(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpDelete("DeleteCart/${id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var response = await _cart.DeleteCart(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region CartItem
        [HttpGet("GetItemsInCart/${id}")]
        public async Task<IActionResult> GetItemsInCart(int id)
        {
            var response = await _cartItem.GetItemsInCart(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPost("AddItemInCart")]

        public async Task<IActionResult> AddItemInCart([FromBody] AddCartItemDto dto)
        {
            var response = await _cartItem.AddItemInCart(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
      
        [HttpDelete("DeleteItemInCart/${id}")]
        public async Task<IActionResult> DeleteItemInCart(int id)
        {
            var response = await _cartItem.DeleteItemInCart(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region Order
        [HttpGet("GetOrder/${id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var response = await _order.GetOrder(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetOrderDetails/${id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var response = await _order.GetOrderDetails(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllOrderforUser/${id}")]
        public async Task<IActionResult> GetAllOrderforUser(int id)
        {
            var response = await _order.GetAllOrderforUser(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(AddOrderDto dto)
        {
            var response = await _order.AddOrder(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto dto)
        {
            var response = await _order.UpdateOrder(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }

        [HttpDelete("DeleteOrder/${id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _order.DeleteOrder(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region Review
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(AddReviewDto dto)
        {

            var response = await _reviewServices.AddReview(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto dto)
        {

            var response = await _reviewServices.UpdateReview(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReview(int id)
        {

            var response = await _reviewServices.DeleteReview(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region Wishlist
        [HttpPost("AddWishList")]
        public async Task<IActionResult> AddWishList(AddWishListDto dto)
        {

            var response = await _wishList.AddWishList(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetWishList/${customerId}")]
        public async Task<IActionResult> GetWishList([FromRoute]int customerId)
        {

            var response = await _wishList.GetWishList(customerId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpDelete("DeleteWishList/${id}")]
        public async Task<IActionResult> DeleteWishList(int id)
        {

            var response = await _wishList.DeleteWishList(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        [HttpGet("GetCustomerProfile/${id}")]
        public async Task<IActionResult> GetCustomerProfile(int id)
        {
            var response = await _authentication.GetCustomerProfile(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
    }
}
