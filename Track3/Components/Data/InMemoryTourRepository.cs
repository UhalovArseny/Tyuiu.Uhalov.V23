using VirtualMuseum.Models;

namespace VirtualMuseum.Data
{
    public class InMemoryTourRepository : ITourRepository
    {
        private readonly List<Tour> _tours = new();

        public Task<IEnumerable<Tour>> GetAllAsync() =>
            Task.FromResult(_tours.AsEnumerable());

        public Task<Tour> GetByIdAsync(Guid id) =>
            Task.FromResult(_tours.FirstOrDefault(t => t.Id == id));

        public Task AddAsync(Tour tour)
        {
            _tours.Add(tour);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Tour tour)
        {
            var existing = _tours.FirstOrDefault(t => t.Id == tour.Id);
            if (existing != null)
            {
                _tours.Remove(existing);
                _tours.Add(tour);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var existing = _tours.FirstOrDefault(t => t.Id == id);
            if (existing != null)
                _tours.Remove(existing);

            return Task.CompletedTask;
        }
    }
}
