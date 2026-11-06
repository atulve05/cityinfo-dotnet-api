namespace CityInfo.Domain.Models
{
    public class PointOfInterestRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
