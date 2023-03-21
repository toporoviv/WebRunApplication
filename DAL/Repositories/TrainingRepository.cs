using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DataEntity;

namespace WebRunApplication.DAL.Repositories
{
    public class TrainingRepository : IBaseRepository<Training>
    {
        private readonly ApplicationDbContext _db;

        public TrainingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Training entity)
        {
            await _db.Trainings.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Training entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Training> GetAll()
        {
            return _db.Trainings;
        }

        public async Task<Training> Update(Training entity)
        {
            _db.Trainings.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
