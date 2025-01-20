using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Mailings;

internal class MailingMessageBuilder
{
    private int? Id { get; set; }
    private int? MailingTopicId { get; set; }
    private string? Message { get; set; }
    private DateTime? Date { get; set; }

    public MailingMessageBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public MailingMessageBuilder WithMailingTopicId(int mailingTopicId)
    {
        MailingTopicId = mailingTopicId;

        return this;
    }

    public MailingMessageBuilder WithMessage(string message)
    {
        Message = message;

        return this;
    }

    public MailingMessageBuilder WithDate(DateTime date)
    {
        Date = date;

        return this;
    }

    public MailingMessage Build()
    {
        return new MailingMessage
        {
            Date = Date ?? DateTime.Now,
            Message = Message ?? string.Join(" ", Faker.Lorem.Words(4)),
            MailingTopicId = MailingTopicId ?? Faker.RandomNumber.Next(1, 100),
            Id = Id ?? Faker.RandomNumber.Next(1, 100)
        };
    }
}