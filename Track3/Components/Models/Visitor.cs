using System;
using System.Collections.Generic; // добавить
using System.ComponentModel.DataAnnotations;
using Track3.Components.Models;

namespace Track3.Components.Models
{
    public class Visitor
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // Списки посещённых объектов
        public List<Guid> VisitedExhibitIds { get; set; } = new();
        public List<Guid> VisitedTourIds { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
    }
}

