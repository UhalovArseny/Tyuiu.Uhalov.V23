using VirtualMuseum.Models;

namespace VirtualMuseum.Data
{
    public class InMemoryExhibitRepository : IExhibitRepository
    {
        private readonly List<Exhibit> _exhibits = new()
        {
            new Exhibit
            {
                Id = Guid.NewGuid(),
                Name = "Маска Тутанхамона",
                Description = "Золотая погребальная маска египетского фараона Тутанхамона.",
                ImagePath = "/images/mask.jpg",
                Era = "Древний Египет",
                Author = "Неизвестный мастер",
                Latitude = 40,   // сверху вниз
                Longitude = 15   // слева направо
            },

            new Exhibit
            {
                Id = Guid.NewGuid(),
                Name = "Звёздная ночь",
                Description = "Одна из самых узнаваемых картин Винсента ван Гога.",
                ImagePath = "/images/vangogh.jpg",
                Era = "1889 год",
                Author = "Винсент ван Гог",
                Latitude = 50,
                Longitude = 70
            },

            new Exhibit
            {
                Id = Guid.NewGuid(),
                Name = "Три богатыря",
                Description = "Картина Виктора Васнецова, изображающая трёх легендарных русских богатырей — Добрыню Никитича, Илью Муромца и Алёшу Поповича. Одно из самых известных произведений русской живописи.",
                ImagePath = "/images/3bogatyrya.jpg", // положи картинку в wwwroot/images/
                Era = "1881–1898 годы",
                Author = "Виктор Михайлович Васнецов",
                Latitude = 75,
                Longitude = 35
            },

            new Exhibit
            {
                Id = Guid.NewGuid(),
                Name = "Мона Лиза",
                Description = "Портрет Лизы Герардини, жены Франческо дель Джокондо. Одна из самых знаменитых картин Леонардо да Винчи.",
                ImagePath = "/images/monalisa.jpg",
                Era = "1503–1506 годы",
                Author = "Леонардо да Винчи",
                Latitude = 58,
                Longitude = 60
            },

            new Exhibit
            {
                Id = Guid.NewGuid(),
                Name = "Терракотовый воин",
                Description = "Фигура одного из солдат китайской терракотовой армии, созданной для охраны гробницы императора Цинь Шихуанди.",
                ImagePath = "/images/warrior.jpg",
                Era = "III век до н.э.",
                Author = "Мастера империи Цинь",
                Latitude = 30,
                Longitude = 30
            }
        };


        public Task<IEnumerable<Exhibit>> GetAllAsync() =>
            Task.FromResult(_exhibits.AsEnumerable());

        public Task<Exhibit> GetByIdAsync(Guid id) =>
            Task.FromResult(_exhibits.FirstOrDefault(e => e.Id == id));

        public Task AddAsync(Exhibit exhibit)
        {
            _exhibits.Add(exhibit);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Exhibit exhibit)
        {
            var existing = _exhibits.FirstOrDefault(e => e.Id == exhibit.Id);
            if (existing != null)
            {
                _exhibits.Remove(existing);
                _exhibits.Add(exhibit);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var existing = _exhibits.FirstOrDefault(e => e.Id == id);
            if (existing != null)
                _exhibits.Remove(existing);

            return Task.CompletedTask;
        }
    }
}
