using VirtualMuseum.Data;
using VirtualMuseum.Models;

namespace VirtualMuseum.Services
{
    public class ExhibitService : IExhibitService
    {
        private readonly IExhibitRepository _repo;

        public ExhibitService(IExhibitRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Exhibit>> GetAllAsync() =>
            _repo.GetAllAsync();

        public Task<Exhibit> GetAsync(Guid id) =>
            _repo.GetByIdAsync(id);

        public Task CreateAsync(Exhibit exhibit) =>
            _repo.AddAsync(exhibit);

        public Task UpdateAsync(Exhibit exhibit) =>
            _repo.UpdateAsync(exhibit);

        public Task DeleteAsync(Guid id) =>
            _repo.DeleteAsync(id);
    }
}
