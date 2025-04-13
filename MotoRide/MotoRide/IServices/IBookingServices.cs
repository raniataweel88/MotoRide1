using Azure;
using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;

namespace MotoRide.IServices
{
    public interface IBookingServices 
    {
        public Task<ServiceResponse> AddBooking(AddBookingDto dto);
        public Task<ServiceResponse> UpdateBooking(UpdateBookingDto dto);
        public Task<ServiceResponse> GetAllBookingNotRejectforMantaince(int maintenanceId);
        public Task<ServiceResponse> GetAllBookingAcceptforMantaince(int maintenanceId);

        public  Task<ServiceResponse> GetAllBookingNotAcceptforMantaince(int maintenanceId);
        public Task<ServiceResponse> GetAllBookingNotRejectforCustomer(int customerId);

        public Task<ServiceResponse> GetAllBookingAcceptforCustomer(int customerId);

        public Task<ServiceResponse> GetAllBookingNotAcceptforCustomer(int customerId);

        public Task<ServiceResponse> AddResponseForBooking(AddResponseBookingDto dto);

        public Task<ServiceResponse> AddResponseForBookingForCustomer(AddResponseCustomerBookingDto dto);
    }
}
