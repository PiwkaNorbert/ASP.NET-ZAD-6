using System;
using System.ComponentModel.DataAnnotations;

namespace lab6API.Models
{
    public class WheaterItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int TemperatureC { get; set; }
        public string summary { get; set; }
    }
}
