using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Mailings;

internal class MailingTopicBuilder
{
    private int? Id { get; set; }
    private string? Title { get; set; }

    public MailingTopicBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public MailingTopicBuilder WithTitle(string title)
    {
        Title = title;

        return this;
    }
    
    public MailingTopic Build()
    {
        return new MailingTopic
        {
            Title = Title ?? Faker.Lorem.GetFirstWord(),
            Id = Id ?? Faker.RandomNumber.Next(0, 100)
        };
    }
}