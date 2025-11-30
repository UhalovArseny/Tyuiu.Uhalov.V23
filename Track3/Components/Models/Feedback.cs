using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualMuseum.Models
{
    public class Feedback
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? ExhibitId { get; set; }
        public Guid? TourId { get; set; }

        public string? VisitorName { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
