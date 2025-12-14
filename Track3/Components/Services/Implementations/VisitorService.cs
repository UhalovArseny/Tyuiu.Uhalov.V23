using Track3.Components.Models;

namespace VirtualMuseum.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly List<Visitor> _visitors = new();

        public Task<IEnumerable<Visitor>> GetAllAsync() =>
            Task.FromResult(_visitors.AsEnumerable());

        public Task AddAsync(Visitor visitor)
        {
            _visitors.Add(visitor);
            return Task.CompletedTask;
        }

        public Task<Visitor> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task BookTourAsync(Guid visitorId, Guid tourId)
        {
            throw new NotImplementedException();
        }

        public Task CancelBookingAsync(Guid visitorId, Guid tourId)
        {
            throw new NotImplementedException();
        }
    }
}
