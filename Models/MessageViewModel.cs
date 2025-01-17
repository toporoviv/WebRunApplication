using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Domain.Enums.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Fullname { get; set; }

        public string Message { get; set; }

        public uint NestingLevel { get; set; }

        public DateTime Date { get; set; }

        public List<User> LikedUsers { get; set; }

        public List<User> DislikedUsers { get; set; }
    }
}
