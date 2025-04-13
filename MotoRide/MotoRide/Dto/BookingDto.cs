using MotoRide.Models;

namespace MotoRide.Dto
{
    public class AddBookingDto
    {
        public string? Title { get; set; }
        
        public DateTime? Date { get; set; }
        public string? CustomerNote { get; set; }
        public int? CustomerId { get; set; }
        public int? MaintenanceId { get; set; }
        public string? MaintenanceNote { get; set; }
        public int? PaymentmethodId { get; set; }
        public string? Location { get; set; }
        public string? BikeType { get; set; } // مثال: "هوائية", "كهربائية"
        public string? ImageUrl { get; set; } // رابط الصورة
        public int? InvoiceId { get; set; }
    }
    public class UpdateBookingDto
    {
        public int BookingId { get; set; }
        public string? Title { get; set; }

        public DateTime? Date { get; set; }
        public string? CustomerNote { get; set; }
        public int? CustomerId { get; set; }
        public int? MaintenanceId { get; set; }
        public string? StatusBookingMaintenance { get; set; }
        public string? MaintenanceNote { get; set; }
        public int? PaymentmethodId { get; set; }
        public string? Location { get; set; }
        public string? BikeType { get; set; } // مثال: "هوائية", "كهربائية"
        public string? ImageUrl { get; set; } // رابط الصورة
        public int? InvoiceId { get; set; }

    }
    public class AddResponseBookingDto
    {
        public int BookingId { get; set; }
        public float? InitialTotalPrice { get; set; }
        public bool? IsAcceptableMaintaince { get; set; }

    }
    public class AddResponseCustomerBookingDto
    {
        public int BookingId { get; set; }
        public bool? IsAcceptableUserIntialPrice { get; set; }

    }
}
