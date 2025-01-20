using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;

namespace WebRunApplication.Infrastructure.Repositories
{
    public class MailingRepository : IBaseRepository<MailingMessage>
    {
        private readonly ApplicationDbContext _db;

        public MailingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<MailingMessage> GetAll()
        {
            return _db.Mailings;
        }

        public async Task Delete(MailingMessage entity)
        {
            _db.Mailings.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(MailingMessage entity)
        {
            await _db.Mailings.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<MailingMessage> Update(MailingMessage entity)
        {
            _db.Mailings.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}