using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoApi.DTOs
{
    public class StateDto
    {
        [Required]
        [StringLength(2)]
        public string StatePostalCode { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Capital { get; set; }
        
        public List<CityDto> Cities { get; set; } = new List<CityDto>();
    }
} 