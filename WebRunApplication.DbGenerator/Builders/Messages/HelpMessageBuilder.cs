using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Messages;

internal class HelpMessageBuilder
{
    private int? Id { get; set; }
    private int? UserId { get; set; }
    private DateTime? Date { get; set; }
    private string? Question { get; set; }
    private string? Answer { get; set; }

    public HelpMessageBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public HelpMessageBuilder WithUserId(int userId)
    {
        UserId = userId;

        return this;
    }

    public HelpMessageBuilder WithDate(DateTime date)
    {
        Date = date;

        return this;
    }

    public HelpMessageBuilder WithQuestion(string question)
    {
        Question = question;

        return this;
    }

    public HelpMessageBuilder WithAnswer(string answer)
    {
        Answer = answer;

        return this;
    }
    
    public HelpMessage Build()
    {
        return new HelpMessage
        {
            Question = Question ?? string.Join(" ", Faker.Lorem.Words(Faker.RandomNumber.Next(3, 10))),
            Answer = Answer ?? string.Join(" ", Faker.Lorem.Words(Faker.RandomNumber.Next(3, 10))),
            Date = Date ?? DateTime.Now,
            Id = Id ?? Faker.RandomNumber.Next(1, 100),
            UserId = UserId ?? Faker.RandomNumber.Next(1, 100)
        };
    }
}