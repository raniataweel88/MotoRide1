namespace MotoRide.Dto
{
    public class AddMaintenanceDto
    {
         public string? MaintenanceName { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Iamgelicense { get; set; }
        public string? Location { get; set; }
    }
}
