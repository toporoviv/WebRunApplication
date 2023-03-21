using System.Diagnostics.CodeAnalysis;

namespace WebRunApplication.DataEntity.Forum
{
    public class ForumMessage
    {
        public uint Id { get; set; }

        [MaybeNull]
        public uint? ParentId { get; set; }

        public DateTime Date { get; set; }

        public uint UserId { get; set; }

        public string Message { get; set; }
    }
}
