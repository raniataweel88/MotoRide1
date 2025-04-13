using INTEGRATEDAPI.Shared;
using Microsoft.EntityFrameworkCore;
using MotoRide.Dto;
using MotoRide.IServices;
using MotoRide.Models;

namespace MotoRide.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly MotoRideDbContext _context;
        private readonly ServiceResponse _response;

        public BookingServices(MotoRideDbContext context,ServiceResponse response)
        {
            _context = context;
            _response = response;
        }
        public async Task<ServiceResponse> AddBooking(AddBookingDto dto)
        {
            try
            {
                Booking booking = new Booking
                {
                    BikeType = dto.BikeType,
                    CreatedAt = DateTime.UtcNow,
                    CustomerNote = dto.CustomerNote,
                    ImageUrl = dto.ImageUrl,
                    MaintenanceNote = dto.MaintenanceNote,
                    Date = dto.Date,
                    Title = dto.Title,
                    StatusBookingMaintenance = "none",
                    IsAcceptableMaintaince = false,
                    CustomerId = dto.CustomerId,
                    MaintenanceId = dto.MaintenanceId,
                    Location = dto.Location,

                };
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                _response.Message = "done to add booking";
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not add booking" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> UpdateBooking(UpdateBookingDto dto) 
        {
            try
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == dto.BookingId);
                if (booking == null)
                  {
                    _response.Success = true;
                    _response.Message = "can not update booking";
                    return _response;

                }
                else
                {
                    booking.BikeType = dto.BikeType;
                    booking.CreatedAt = DateTime.UtcNow;
                    booking.CustomerNote = dto.CustomerNote;
                    booking.ImageUrl = dto.ImageUrl;
                    booking.MaintenanceNote = dto.MaintenanceNote;
                    booking.Date = dto.Date;
                    booking.Title = dto.Title;
                    booking.StatusBookingMaintenance = dto.StatusBookingMaintenance;
                    booking.IsAcceptableMaintaince = false;
                    booking.CustomerId = dto.CustomerId;
                    booking.MaintenanceId = dto.MaintenanceId;
                    booking.Location = dto.Location;
                    _context.Bookings.Update(booking);
                    await _context.SaveChangesAsync();
                    _response.Message = "done to add booking";
                    _response.Success = true;
                    return _response;
                }
            }
            catch (Exception e)
            {
                _response.Message = $"can not add booking" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> GetAllBookingNotRejectforMantaince(int maintenanceId)
        {
            try
            {
                var booking = await _context.Bookings.Where(x => x.IsAcceptableMaintaince == null&&x.MaintenanceId== maintenanceId).ToListAsync();
                _response.Data = booking;
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get any  booking for this maintenance" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> GetAllBookingAcceptforMantaince(int maintenanceId)
        {
            try
            {
                var booking = await _context.Bookings.Where(x => x.IsAcceptableMaintaince == true && x.MaintenanceId == maintenanceId).ToListAsync();
                _response.Data = booking;
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get any  booking for this maintenance" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> GetAllBookingNotAcceptforMantaince(int maintenanceId)
        {
            try
            {
                var booking = await _context.Bookings.Where(x => x.IsAcceptableMaintaince == false && x.MaintenanceId == maintenanceId).ToListAsync();
                _response.Data = booking;
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get any  booking for this maintenance" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> GetAllBookingNotRejectforCustomer(int customerId)
        {
            try
            {
                var booking = await _context.Bookings.Where(x => x.IsAcceptableMaintaince == null && x.CustomerId == customerId).ToListAsync();
                _response.Data = booking;
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get any  booking for this customer" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> GetAllBookingAcceptforCustomer(int customerId)
        {
            try
            {
                var booking = await _context.Bookings.Where(x => x.IsAcceptableMaintaince == true && x.CustomerId == customerId).ToListAsync();
                _response.Data = booking;
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get any  booking for this maintenance" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> GetAllBookingNotAcceptforCustomer(int customerId)
        {
            try
            {
                var booking = await _context.Bookings.Where(x => x.IsAcceptableMaintaince == false && x.CustomerId == customerId).ToListAsync();
                _response.Data = booking;
                _response.Success = true;
                return _response;
            }
            catch (Exception e)
            {
                _response.Message = $"can not get any  booking for this " + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> AddResponseForBooking(AddResponseBookingDto dto)
        {
            try
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == dto.BookingId);
                if (booking == null)
                {
                    _response.Success = true;
                    _response.Message = "can not update booking";
                    return _response;

                }
                else
                {
                    booking.InitialTotalPrice = dto.InitialTotalPrice;
                    booking.IsAcceptableMaintaince = dto.IsAcceptableMaintaince;

                 
                    _context.Bookings.Update(booking);
                    await _context.SaveChangesAsync();
                    _response.Message = "done to add booking";
                    _response.Success = true;
                    return _response;
                }
            }
            catch (Exception e)
            {
                _response.Message = $"can not add booking" + e.Message;
                _response.Success = false;
                return _response;
            }
        }
        public async Task<ServiceResponse> AddResponseForBookingForCustomer(AddResponseCustomerBookingDto dto)
        {
            try
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == dto.BookingId);
                if (booking == null)
                {
                    _response.Success = true;
                    _response.Message = "can not update booking";
                    return _response;

                }
                else
                {
                    booking.IsAcceptableUserIntialPrice = dto.IsAcceptableUserIntialPrice;


                    _context.Bookings.Update(booking);
                    await _context.SaveChangesAsync();
                    _response.Message = "done to add booking";
                    _response.Success = true;
                    return _response;
                }
            }
            catch (Exception e)
            {
                _response.Message = $"can not add booking" + e.Message;
                _response.Success = false;
                return _response;
            }
        }


    }
}
