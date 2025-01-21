namespace WebRunApplication.Infrastructure.Models;

public sealed record ForumMessage
{
    public int? ParentId { get; set; }
    public required DateTime Date { get; set; }
    public required int UserId { get; set; }
    public required string Message { get; set; }
}