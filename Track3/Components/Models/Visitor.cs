using System;
using System.Collections.Generic; // добавить
using System.ComponentModel.DataAnnotations;

namespace VirtualMuseum.Models
{
    public class Visitor
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        // Новое: на какие туры записан посетитель
        public List<Guid> BookedTourIds { get; set; } = new();
    }
}
