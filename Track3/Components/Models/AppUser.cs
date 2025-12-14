using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Track3.Components.Models
{
    public class AppUser
    {
        [Key] public int Id { get; set; }

        [Required, MaxLength(64)]
        public string UserName { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginUtc { get; set; }

        public List<VisitedExhibit> VisitedExhibits { get; set; } = new();
        public List<VisitedTour> VisitedTours { get; set; } = new();
    }

    public class VisitedExhibit
    {
        [Key]
        public int Id { get; set; }

        // FK → пользователь
        public int UserId { get; set; }

        // FK → экспонат
        public Guid ExhibitId { get; set; }

        public DateTime VisitedAtUtc { get; set; } = DateTime.UtcNow;

        // навигация (необязательно, но полезно)
        [ForeignKey(nameof(UserId))]
        public AppUser? User { get; set; }
    }

    public class VisitedTour
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public Guid TourId { get; set; }

        public DateTime VisitedAtUtc { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public AppUser? User { get; set; }
    }
}
