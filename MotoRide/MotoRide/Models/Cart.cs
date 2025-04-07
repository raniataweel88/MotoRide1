
namespace MotoRide.Models
{

    public class Cart
    {
        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public User? Customer { get; set; }
        public List<CartItem>? CartItem { get; set; }
        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
