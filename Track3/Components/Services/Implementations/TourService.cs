using VirtualMuseum.Data;
using VirtualMuseum.Models;
using VirtualMuseum.Services;

public class TourService : ITourService
{
    private readonly ITourRepository _repo;

    public TourService(ITourRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Tour>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Tour> GetAsync(Guid id) => _repo.GetByIdAsync(id);
    public Task CreateAsync(Tour tour) => _repo.AddAsync(tour);
    public Task UpdateAsync(Tour tour) => _repo.UpdateAsync(tour);
    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}
