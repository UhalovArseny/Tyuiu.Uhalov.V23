using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace VirtualMuseum.Models
{
    public class Tour
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime? StartTimeUtc { get; set; }
        public int DurationMinutes { get; set; } = 60;

        // ✅ хранится в БД
        public string ExhibitIdsJson { get; set; } = "[]";

        // ✅ удобно в коде, но EF это не мапит
        [NotMapped]
        public List<Guid> ExhibitIds
        {
            get => JsonSerializer.Deserialize<List<Guid>>(ExhibitIdsJson) ?? new();
            set => ExhibitIdsJson = JsonSerializer.Serialize(value ?? new());
        }
    }
}
