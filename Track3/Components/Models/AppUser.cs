using System.ComponentModel.DataAnnotations;

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
        [Key] public int Id { get; set; }
        public Guid ExhibitId { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public DateTime VisitedAtUtc { get; set; } = DateTime.UtcNow;
    }

    public class VisitedTour
    {
        [Key] public int Id { get; set; }
        public Guid TourId { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public DateTime VisitedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
