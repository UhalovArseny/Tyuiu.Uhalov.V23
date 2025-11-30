using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualMuseum.Models
{
    public class Exhibit
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        // Координаты на карте (необязательные)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string Era { get; set; }
        public string Author { get; set; }
    }
}
