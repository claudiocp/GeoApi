using System.ComponentModel.DataAnnotations;

namespace GeoApi.DTOs
{
    public class CityDto
    {
        [Required]
        public string City { get; set; }
        
        [Required]
        public double Longitude { get; set; }
        
        [Required]
        public double Latitude { get; set; }
    }
} 