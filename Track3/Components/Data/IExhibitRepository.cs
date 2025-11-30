using VirtualMuseum.Models;

namespace VirtualMuseum.Data
{
    public interface IExhibitRepository
    {
        Task<IEnumerable<Exhibit>> GetAllAsync();
        Task<Exhibit> GetByIdAsync(Guid id);
        Task AddAsync(Exhibit exhibit);
        Task UpdateAsync(Exhibit exhibit);
        Task DeleteAsync(Guid id);
    }
}
