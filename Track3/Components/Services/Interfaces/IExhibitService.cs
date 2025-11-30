using VirtualMuseum.Models;

namespace VirtualMuseum.Services
{
    public interface IExhibitService
    {
        Task<IEnumerable<Exhibit>> GetAllAsync();
        Task<Exhibit> GetAsync(Guid id);
        Task CreateAsync(Exhibit exhibit);
        Task UpdateAsync(Exhibit exhibit);
        Task DeleteAsync(Guid id);
    }
}
