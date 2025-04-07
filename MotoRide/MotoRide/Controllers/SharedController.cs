using Azure;
using INTEGRATEDAPI.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;
using MotoRide.Services;
using System.Net;

namespace MotoRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private readonly IAuthenticationServices _authservices;
        private readonly ICatrgoiresServices _categoryServices;
        private readonly ISubCategoryServices _subCategoryServices;
        private readonly IStoreServices _StoreServices;
        private readonly IReviewServices _reviewServices;

        public SharedController(IAuthenticationServices authservices, ICatrgoiresServices categoryServices, ISubCategoryServices subCategoryServices, IStoreServices storeServices, IReviewServices reviewServices)
        {
            _authservices = authservices;
            _categoryServices = categoryServices;
            _subCategoryServices = subCategoryServices;
            _StoreServices = storeServices;
            _reviewServices = reviewServices;
        }
        [HttpGet("GetAllSubCategory")]
        public async Task<IActionResult> GetAllSubCategory()
        {
            var response = await _subCategoryServices.GetAllSubCategory();
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #region Authntication
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] AddCustomerDto dto)
        {
            var response = await _authservices.RegisterCustomer(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);

        }
        [HttpPost("RegisterOwnerShop")]
        public async Task<IActionResult> RegisterOwnerShop([FromBody] AddOwnerShopDto dto)
        {
            var response = await _authservices.RegisterOwnerShop(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPut("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _authservices.Login(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);

        }
        [HttpPut("Logout")]
        public async Task<IActionResult> Logout(int id)
        {
            var response = await _authservices.Logout(id);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);

        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string pass)
        {
            var response = await _authservices.ResetPassword(email, pass);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);

        }
        #endregion
        #region Category
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryServices.GetAllCatrgoires();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var response = await _categoryServices.GetCatrgoires(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion
        [HttpGet("GetAllShop")]
        public async Task<IActionResult> GetAllShop()
        {
            var response = await _StoreServices.GetAllStore();
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpPut("AllowStoreToLogin")]
        public async Task<IActionResult> AllowStoreToLogin(int id,bool isAllowLogin)
        {
            var response = await _StoreServices.AllowStoreToLogin(id,isAllowLogin);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #region Review
        [HttpGet("GetAllReviewForThisItem")]
        public async Task<IActionResult> GetAllReviewForThisItem(int? productId, int? motorcycleId)
        {

            var response = await _reviewServices.GetAllReviewForThisItem(productId, motorcycleId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetReviewByShop/${shopId}")]
        public async Task<IActionResult> GetReviewByShop(int shopId, int motorcycleId, int productId)
        {

            var response = await _reviewServices.GetReviewByShop(shopId, motorcycleId, productId);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetDeleteReview")]
        public async Task<IActionResult> GetDeleteReview(GetDeleteStoreDto dto)
        {

            var response = await _reviewServices.GetDeleteReview(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
    }

}