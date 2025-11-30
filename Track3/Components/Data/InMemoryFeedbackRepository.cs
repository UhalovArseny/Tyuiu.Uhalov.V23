using VirtualMuseum.Models;

namespace VirtualMuseum.Data
{
    public class InMemoryFeedbackRepository : IFeedbackRepository
    {
        private readonly List<Feedback> _feedback = new();

        public Task<IEnumerable<Feedback>> GetAllAsync() =>
            Task.FromResult(_feedback.AsEnumerable());

        public Task<IEnumerable<Feedback>> GetByExhibitIdAsync(Guid exhibitId) =>
            Task.FromResult(_feedback
                .Where(f => f.ExhibitId == exhibitId)
                .AsEnumerable());

        public Task<IEnumerable<Feedback>> GetByTourIdAsync(Guid tourId) =>
            Task.FromResult(_feedback
                .Where(f => f.TourId == tourId)
                .AsEnumerable());

        public Task AddAsync(Feedback feedback)
        {
            _feedback.Add(feedback);
            return Task.CompletedTask;
        }
    }
}
