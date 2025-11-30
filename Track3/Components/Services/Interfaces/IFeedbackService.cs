using VirtualMuseum.Models;

namespace VirtualMuseum.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<IEnumerable<Feedback>> GetByExhibitIdAsync(Guid exhibitId);
        Task<IEnumerable<Feedback>> GetByTourIdAsync(Guid tourId);
        Task AddAsync(Feedback feedback);
    }
}

