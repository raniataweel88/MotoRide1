namespace MotoRide.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string? Name { get; set; }
        public int? ShopId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
