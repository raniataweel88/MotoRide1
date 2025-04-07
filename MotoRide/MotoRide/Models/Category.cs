
namespace MotoRide.Models
{

    public class Category 
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<Motorcycle>? MotorcycleList { get; set; }
        public List<Product>? ProductList { get; set; }
        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
