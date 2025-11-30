using VirtualMuseum.Data;
using VirtualMuseum.Models;

namespace VirtualMuseum.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;

        public FeedbackService(IFeedbackRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Feedback>> GetAllAsync() =>
            _repo.GetAllAsync();

        public Task<IEnumerable<Feedback>> GetByExhibitIdAsync(Guid exhibitId) =>
            _repo.GetByExhibitIdAsync(exhibitId);

        public Task<IEnumerable<Feedback>> GetByTourIdAsync(Guid tourId) =>
            _repo.GetByTourIdAsync(tourId);

        public Task AddAsync(Feedback feedback) =>
            _repo.AddAsync(feedback);
    }
}
