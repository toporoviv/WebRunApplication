namespace WebRunApplication.Infrastructure.Models;

public sealed record HelpMessage
{
    public required int UserId { get; set; }
    public required DateTime Date { get; set; }
    public required string Question { get; set; }
    public string? Answer { get; set; }
}