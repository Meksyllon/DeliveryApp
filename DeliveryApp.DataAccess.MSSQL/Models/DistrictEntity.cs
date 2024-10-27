namespace DeliveryApp.Models
{
    public class DistrictEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<PackageEntity> Packages { get; set; }
    }    
}
