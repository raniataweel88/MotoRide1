using System.ComponentModel.DataAnnotations;
using static MotoRide.Helper.Enum;

namespace MotoRide.Models
{
    public class Paymentmethod
    {
        public int Id { get; set; }
        public string? CardNumber { get; set; }
        public string? Code { get; set; }
        public string? CardHolder { get; set; }
        public DateTime? ExpireDate { get; set; }
        public float? Blane { get; set; }

        public PaymentMethod Method { get; set; }
    }
}
