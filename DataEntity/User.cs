using System.Diagnostics.CodeAnalysis;
using WebRunApplication.Enums;

namespace WebRunApplication.DataEntity
{
    public class User
    {
        public uint Id { get; set; } 

        public string Login { get; set; }

        public string Password { get; set; }

        [MaybeNull]
        public string Email { get; set; }

        public string Fullname { get; set; }

        public uint Age { get; set; }

        public Gender Gender { get; set; }

        public uint Weight { get; set; }

        public uint Height { get; set; }

        public Role Role { get; set; }
    }
}
