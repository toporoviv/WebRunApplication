using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DataEntity;

namespace WebRunApplication.DAL.Repositories
{
    public class TrainingTemplateRepository : IBaseRepository<TrainingTemplate>
    {
        private readonly ApplicationDbContext _db;

        public TrainingTemplateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(TrainingTemplate entity)
        {
            await _db.TrainingTemplates.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TrainingTemplate entity)
        {
            _db.TrainingTemplates.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<TrainingTemplate> GetAll()
        {
            return _db.TrainingTemplates;
        }

        public async Task<TrainingTemplate> Update(TrainingTemplate entity)
        {
            _db.TrainingTemplates.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
