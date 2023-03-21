using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DataEntity;

namespace WebRunApplication.DAL.Repositories
{
    public class IndicatorRepository : IBaseRepository<Indicator>
    {
        private readonly ApplicationDbContext _db;

        public IndicatorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Indicator entity)
        {
            await _db.Indicators.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Indicator entity)
        {
            _db.Indicators.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Indicator> GetAll()
        {
            return _db.Indicators;
        }

        public async Task<Indicator> Update(Indicator entity)
        {
            _db.Indicators.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
