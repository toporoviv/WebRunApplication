namespace WebRunApplication.Domain.Entities;

public class MailingTopicSubscriber
{
    public int Id { get; set; }

    public int MailingTopicId { get; set; }

    public int UserId { get; set; }
}