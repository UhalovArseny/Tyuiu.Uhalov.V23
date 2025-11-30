using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualMuseum.Models
{
    public class Tour
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        // Храним список ID экспонатов
        public List<Guid> ExhibitIds { get; set; } = new();

        public TimeSpan? Duration { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
