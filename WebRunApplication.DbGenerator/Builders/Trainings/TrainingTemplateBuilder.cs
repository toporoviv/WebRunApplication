using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Trainings;

internal class TrainingTemplateBuilder
{
    private int? Id { get; set; }
    private string? Title { get; set; }

    public TrainingTemplateBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public TrainingTemplateBuilder WithTitle(string title)
    {
        Title = title;

        return this;
    }
    
    public TrainingTemplate Build()
    {
        return new TrainingTemplate
        {
            Title = Title ?? Faker.Lorem.GetFirstWord(),
            Id = Id ?? Faker.RandomNumber.Next(0, 100)
        };
    }
}