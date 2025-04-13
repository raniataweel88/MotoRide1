using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using MotoRide.Services;

namespace MotoRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopOwnerController : ControllerBase
    {
        private readonly IAuthenticationServices _authentication;
        private readonly IProductSerives _productSerives;
        private readonly IMotocyleService _motocyleService;
        private readonly ISubCategoryServices _subCategoryServices;
        private readonly IOrderServices _orderServices;
        private readonly ImageServices imageServices;
        private readonly IReviewServices _reviewServices;

        public ShopOwnerController(IAuthenticationServices authentication, IProductSerives productSerives, IMotocyleService motocyleService,ImageServices image,
            ISubCategoryServices subCategory, IOrderServices orderServices, IReviewServices reviewServices)
        {
            _authentication = authentication;
            imageServices = image;
            _productSerives = productSerives;
            _motocyleService = motocyleService;
            _subCategoryServices = subCategory;
            _orderServices = orderServices;
            _reviewServices = reviewServices;
        }


        #region product
        [HttpGet("GetProductByShop/${id}")]
        public async Task<IActionResult> GetProductByShop(int id)
        {
            var response = await _productSerives.GetProductByShop(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPost("AddProduct")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto dto,  IFormFile images)
        {
            var response = await _productSerives.AddProduct(dto, images);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut("UpdateProduct")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto dto, IFormFile? images)
        {
            var response = await _productSerives.UpdateProduct(dto, images);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpDelete("DeleteProduct/${id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productSerives.DeleteProduct(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region  Motocyle
        [HttpGet("GetMotocyleByShop/${id}")]
        public async Task<IActionResult> GetMotorcycleByShop(int id)
        {
            var response = await _motocyleService.GetMotorcycleByShop(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPost("AddMotorcycle")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddMotorcycle([FromForm] AddMotorcycleDto dto, IFormFile? images)
        {
            var response = await _motocyleService.AddMotorcycle(dto, images);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPut("UpdateMotorcycle")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMotorcycle([FromForm] UpdateMotorcycleDto dto ,IFormFile? images)
        {
            var response = await _motocyleService.UpdateMotorcycle(dto, images);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpDelete("DeleteMotorcycle/${id}")]
        public async Task<IActionResult> DeleteMotorcycle(int id)
        {
            var response = await _motocyleService.DeleteMotorcycle(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region SubCategory
        [HttpGet("GetSubCategory/${id}")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            var response = await _subCategoryServices.GetSubCategory(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
       
        [HttpGet("GetAllSubCategoryByShop/${id}")]
        public async Task<IActionResult> GetAllSubCategoryByShop(int id)
        {
            var response = await _subCategoryServices.GetAllSubCategoryByShop(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPost("AddSubCategory")]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCategoryDto sc)
        {
            var response = await _subCategoryServices.AddSubCategory(sc);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPut("UpdateSubCategory")]
        public async Task<IActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryDto sc)
        {
            var response = await _subCategoryServices.UpdateSubCategory(sc);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpDelete("DeleteSubCategory/${id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var response = await _subCategoryServices.DeleteSubCategory(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }

        #endregion
        #region Order
        [HttpGet("GetAllOrderForthisShop/${orderId}")]
        public async Task<IActionResult> GetAllOrderForthisShop(int orderId)
        {
            var response = await _orderServices.GetAllOrderForthisShop(orderId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllOrderNotReceivedByShop/${shopId}")]
        public async Task<IActionResult> GetAllOrderNotReceivedByShop(int shopId)
        {
            var response = await _orderServices.GetAllOrderNotReceivedByShop(shopId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllOrderReceivedByShop/${shopId}")]
        public async Task<IActionResult> GetAllOrderReceivedByShop(int shopId)
        {
            var response = await _orderServices.GetAllOrderReceivedByShop(shopId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetOrderByIdForthisShop/${orderId}/${shopId}")]
        public async Task<IActionResult> GetOrderByIdForthisShop(int orderId,int shopId)
        {
            var response = await _orderServices.GetOrderByIdForthisShop(orderId, shopId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetItemOrderNotReceivedByShop/${orderId}/${shopId}")]
        public async Task<IActionResult> GetItemOrderNotReceivedByShop(int orderId, int shopId)
        {
            var response = await _orderServices.GetItemOrderNotReceivedByShop(orderId, shopId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetItemOrderReceivedByShop/${orderId}/${shopId}")]
        public async Task<IActionResult> GetItemOrderReceivedByShop(int orderId, int shopId)
        {
            var response = await _orderServices.GetItemOrderReceivedByShop(orderId, shopId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }

        #endregion
        #region Review
        [HttpPut("DeleteReviewByStore")]
        public async Task<IActionResult> DeleteReviewByStore(DeleteRviewByStoreDto dto)
        {

            var response = await _reviewServices.DeleteReviewByStore(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion

    }
}