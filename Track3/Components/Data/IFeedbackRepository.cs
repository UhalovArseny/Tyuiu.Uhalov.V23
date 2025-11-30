using VirtualMuseum.Models;

namespace VirtualMuseum.Data
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<IEnumerable<Feedback>> GetByExhibitIdAsync(Guid exhibitId);
        Task<IEnumerable<Feedback>> GetByTourIdAsync(Guid tourId);
        Task AddAsync(Feedback feedback);
    }
}
