using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;

namespace WebRunApplication.Infrastructure.Repositories
{
    public class HelpRepository : IBaseRepository<HelpMessage>
    {
        private readonly ApplicationDbContext _db;

        public HelpRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<HelpMessage> GetAll()
        {
            return _db.Helps;
        }

        public async Task Delete(HelpMessage entity)
        {
            _db.Helps.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(HelpMessage entity)
        {
            await _db.Helps.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<HelpMessage> Update(HelpMessage entity)
        {
            _db.Helps.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}