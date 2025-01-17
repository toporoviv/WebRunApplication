namespace WebRunApplication.Domain.Entities;

public class Mailing
{
    public int Id { get; set; }

    public int MailingTopicId { get; set; }

    public required string Message { get; set; }

    public required DateTime Date { get; set; }
}