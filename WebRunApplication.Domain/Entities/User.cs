using WebRunApplication.Domain.Enums;

namespace WebRunApplication.Domain.Entities
{
    public class User
    {
        public int Id { get; set; } 

        public required string Login { get; set; }

        public required string Password { get; set; }
        
        public string Email { get; set; }

        public required string Fullname { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public uint Weight { get; set; }

        public uint Height { get; set; }

        public Role Role { get; set; }
    }
}
