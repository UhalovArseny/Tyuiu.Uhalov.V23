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

public interface ICurrentVisitorService
{
    Visitor? Current { get; }

    Task<(bool Success, string? Error)> LoginAsync(string name, string password);
    void Logout();

    Task MarkExhibitVisitedAsync(Guid exhibitId);
    Task MarkTourVisitedAsync(Guid tourId);

    bool HasVisitedExhibit(Guid exhibitId);
    bool HasVisitedTour(Guid tourId);
}

public class CurrentVisitorService : ICurrentVisitorService
{
    private readonly List<Visitor> _visitors = new();

    public Visitor? Current { get; private set; }

    public Task<(bool Success, string? Error)> LoginAsync(string name, string password)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
        {
            return Task.FromResult((false, "Имя и пароль не могут быть пустыми"));
        }

        name = name.Trim();

        var existing = _visitors.FirstOrDefault(v => v.Name == name);

        if (existing == null)
        {
            // Если пользователя с таким именем нет — регистрируем нового
            existing = new Visitor
            {
                Name = name,
                Password = password, // в реальном проекте тут был бы hash
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            };
            _visitors.Add(existing);
        }
        else
        {
            // Проверяем пароль
            if (existing.Password != password)
            {
                return Task.FromResult((false, "Неверный пароль"));
            }

            existing.LastLoginAt = DateTime.UtcNow;
        }

        Current = existing;
        return Task.FromResult((true, null as string));
    }

    public void Logout()
    {
        Current = null;
    }

    public Task MarkExhibitVisitedAsync(Guid exhibitId)
    {
        if (Current != null && !Current.VisitedExhibitIds.Contains(exhibitId))
            Current.VisitedExhibitIds.Add(exhibitId);

        return Task.CompletedTask;
    }

    public Task MarkTourVisitedAsync(Guid tourId)
    {
        if (Current != null && !Current.VisitedTourIds.Contains(tourId))
            Current.VisitedTourIds.Add(tourId);

        return Task.CompletedTask;
    }

    public bool HasVisitedExhibit(Guid exhibitId) =>
        Current != null && Current.VisitedExhibitIds.Contains(exhibitId);

    public bool HasVisitedTour(Guid tourId) =>
        Current != null && Current.VisitedTourIds.Contains(tourId);
}