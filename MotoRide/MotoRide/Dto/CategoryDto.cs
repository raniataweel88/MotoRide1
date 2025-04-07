namespace MotoRide.Dto
{
    public class CategoryDto
    {

        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }
    public class SubCategoryDto
    {
        public string? Name { get; set; }
        public int? ShopId { get; set; }
    }
    public class UpdateSubCategoryDto
    {
        public int SubCategoryId { get; set; }
        public string? Name { get; set; }
        public int? ShopId { get; set; }
    }
    
}
