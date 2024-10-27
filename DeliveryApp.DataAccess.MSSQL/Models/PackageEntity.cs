namespace DeliveryApp.Models
{
    public class PackageEntity
    {
        public int Id { get; set; }

        public float Weight { get; set; }

        public DateTime OrderTime { get; set; }

        public int DistrictId { get; set; }

        public DistrictEntity? District { get; set; }
    }
}
