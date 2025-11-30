using VirtualMuseum.Models;

namespace VirtualMuseum.Data
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour> GetByIdAsync(Guid id);
        Task AddAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(Guid id);
    }
}
