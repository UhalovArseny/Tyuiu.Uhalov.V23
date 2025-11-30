using VirtualMuseum.Models;

namespace VirtualMuseum.Services
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour> GetAsync(Guid id);
        Task CreateAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(Guid id);
    }
}
