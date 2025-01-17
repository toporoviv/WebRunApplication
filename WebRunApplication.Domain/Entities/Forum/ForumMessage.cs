namespace WebRunApplication.Domain.Entities.Forum
{
    public class ForumMessage
    {
        public int Id { get; set; }
        
        public int? ParentId { get; set; }

        public required DateTime Date { get; set; }

        public required int UserId { get; set; }

        public required string Message { get; set; }
    }
}
