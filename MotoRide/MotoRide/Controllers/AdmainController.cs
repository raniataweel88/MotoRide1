using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdmainController : ControllerBase
    {
        private readonly ICatrgoiresServices _categoryServices;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IReviewServices _reviewServices;
        private readonly IStoreServices _storeServices; 
        public AdmainController(ICatrgoiresServices categoryServices,IAuthenticationServices authenticationServices, IReviewServices reviewServices,IStoreServices store)
        {
            _categoryServices = categoryServices;
            _authenticationServices = authenticationServices;
            _reviewServices = reviewServices;
            _storeServices=store;
        }
        #region Category
   
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(string name) { 
            var response = await _categoryServices.AddCatrgoires(name);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category)
        {
            var response = await _categoryServices.UpdateCatrgoires(category);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryServices.DeleteCatrgoires(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion
        #region Customer
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddCustomerDto dto)
        {
            var response = await _authenticationServices.AddUser(dto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion
        #region Review
        [HttpPut("DeleteReviewByAdmain")]
        public async Task<IActionResult> DeleteReviewByAdmain(DeleteRviewByAdmainDto dto)
        {

            var response = await _reviewServices.DeleteReviewByAdmain(dto);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
        #region Store
        [HttpGet("GetAllStoreAccept")]
        public async Task<IActionResult> GetAllStoreAccept()
        {
            var response = await _storeServices.GetAllStoreAccept();
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }

        [HttpGet("GetAllStoreNotResponse")]
        public async Task<IActionResult> GetAllStoreNotResponse()
        {
            var response = await _storeServices.GetAllStoreNotResponse();
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("GetAllStoreReject")]
        public async Task<IActionResult> GetAllStoreReject()
        {
            var response = await _storeServices.GetAllStoreReject();
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        [HttpGet("AllowStoreToLogin")]
        public async Task<IActionResult> AllowStoreToLogin(int id,bool IsCanLogin)
        {
            var response = await _storeServices.AllowStoreToLogin(id,IsCanLogin);
            if (response.Success) { return Ok(response); }
            return BadRequest(response);
        }
        #endregion
    }
}
