using Track3.Components.Models;

namespace VirtualMuseum.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetAllAsync();
        Task<Visitor> GetAsync(Guid id);
        Task AddAsync(Visitor visitor);
        Task BookTourAsync(Guid visitorId, Guid tourId);
        Task CancelBookingAsync(Guid visitorId, Guid tourId);
    }

}
