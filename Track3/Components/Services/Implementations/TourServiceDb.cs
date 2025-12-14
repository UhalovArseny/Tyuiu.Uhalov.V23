using Microsoft.EntityFrameworkCore;
using Track3.Components.Data;
using VirtualMuseum.Models;
using VirtualMuseum.Services;

namespace Track3.Components.Services.Implementations
{
    public class TourServiceDb : ITourService
    {
        private readonly AppDbContext _db;

        public TourServiceDb(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Tour>> GetAllAsync()
            => await _db.Tours
                .OrderByDescending(t => t.StartTimeUtc)
                .ToListAsync();

        public async Task<Tour> GetAsync(Guid id)
            => await _db.Tours.FirstAsync(t => t.Id == id);

        public async Task CreateAsync(Tour tour)
        {
            _db.Tours.Add(tour);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tour tour)
        {
            _db.Tours.Update(tour);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var t = await _db.Tours.FirstOrDefaultAsync(x => x.Id == id);
            if (t == null) return;
            _db.Tours.Remove(t);
            await _db.SaveChangesAsync();
        }
    }
}
