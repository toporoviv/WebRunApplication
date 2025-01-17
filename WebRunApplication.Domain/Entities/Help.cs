namespace WebRunApplication.Domain.Entities;

public class Help
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public required string Question { get; set; }
        
    public string? Answer { get; set; }
}