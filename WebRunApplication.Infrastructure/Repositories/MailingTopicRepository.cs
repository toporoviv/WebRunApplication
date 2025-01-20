using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;

namespace WebRunApplication.Infrastructure.Repositories
{
    public class MailingTopicRepository : IBaseRepository<MailingTopic>
    {
        private readonly ApplicationDbContext _db;

        public MailingTopicRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<MailingTopic> GetAll()
        {
            return _db.MailingTopics;
        }

        public async Task Delete(MailingTopic entity)
        {
            _db.MailingTopics.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Create(MailingTopic entity)
        {
            await _db.MailingTopics.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<MailingTopic> Update(MailingTopic entity)
        {
            _db.MailingTopics.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

