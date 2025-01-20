using WebRunApplication.Domain.Entities.Forum;

namespace WebRunApplication.DbGenerator.Builders.Messages;

internal class ForumMessageBuilder
{
    private int? Id { get; set; }
    private int? ParentId { get; set; }
    private DateTime? Date { get; set; }
    private int? UserId { get; set; }
    private string? Message { get; set; }

    public ForumMessageBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public ForumMessageBuilder WithParentId(int? parentId)
    {
        ParentId = parentId;

        return this;
    }

    public ForumMessageBuilder WithDate(DateTime date)
    {
        Date = date;

        return this;
    }

    public ForumMessageBuilder WithUserId(int userId)
    {
        UserId = userId;

        return this;
    }

    public ForumMessageBuilder WithMessage(string message)
    {
        Message = message;

        return this;
    }
    
    public ForumMessage Build()
    {
        return new ForumMessage
        {
            Date = Date ?? DateTime.Now,
            Message = Message ?? string.Join(" ", Faker.Lorem.Words(5)),
            UserId = UserId ?? Faker.RandomNumber.Next(1, 100),
            ParentId = ParentId,
            Id = Id ?? Faker.RandomNumber.Next(1, 100)
        };
    }
}