using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoApi.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(2)]
        public string StatePostalCode { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Capital { get; set; }
        
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
} 