using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Mailings;

internal class MailingTopicSubscriberBuilder
{
    private int? Id { get; set; }
    private int? MailingTopicId { get; set; }
    private int? UserId { get; set; }

    public MailingTopicSubscriberBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public MailingTopicSubscriberBuilder WithMailingTopicId(int mailingTopicId)
    {
        MailingTopicId = mailingTopicId;

        return this;
    }

    public MailingTopicSubscriberBuilder WithUserId(int userId)
    {
        UserId = userId;

        return this;
    }
    
    public MailingTopicSubscriber Build()
    {
        return new MailingTopicSubscriber
        {
            UserId = UserId ?? Faker.RandomNumber.Next(1, 100),
            MailingTopicId = MailingTopicId ?? Faker.RandomNumber.Next(1, 100),
            Id = Id ?? Faker.RandomNumber.Next(1, 100)
        };
    }
}