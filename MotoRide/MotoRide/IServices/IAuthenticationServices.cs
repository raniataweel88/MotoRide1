﻿using INTEGRATEDAPI.Shared;
using MotoRide.Dto;

namespace MotoRide.IServices
{
    public interface IAuthenticationServices
    {
        public Task<ServiceResponse> Login(LoginDto dto);
        public Task<ServiceResponse> RegisterCustomer(AddCustomerDto dto);
        public  Task<ServiceResponse> AddUser(AddCustomerDto dto);
       public Task<ServiceResponse> RegisterOwnerShop(AddOwnerShopDto dto);
        public Task<ServiceResponse> ResetPassword(string email, string newPassword);
        public Task<ServiceResponse> Logout(int id);
       
    }
}
