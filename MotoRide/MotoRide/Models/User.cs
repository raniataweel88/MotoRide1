using static MotoRide.Helper.Enum;

namespace MotoRide.Models
{
    public class User 
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public List<Cart>? Cart { get; set; }
        public List<Order>? Order { get; set; }
        public List<WishList>? WishList { get; set; }
        public int Points { get; set; } = 0; // النقاط المبدئية
        public UserLevel? Level { get; set; } = UserLevel.Basic; // المستوى المبدئي
        public DateTime? CreatedAt { get; set; }
        public string? UserType { get; set; }

        public bool? IsActive { get; set; }
        public string? Token { get; internal set; }
    }
}
