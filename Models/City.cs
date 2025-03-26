using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoApi.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public double Longitude { get; set; }
        
        [Required]
        public double Latitude { get; set; }
        
        [Required]
        public string StatePostalCode { get; set; }
        
        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }
        
        public State State { get; set; }
    }
} 