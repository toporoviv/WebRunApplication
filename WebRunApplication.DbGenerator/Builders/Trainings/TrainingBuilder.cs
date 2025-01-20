using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Trainings;

internal class TrainingBuilder
{
    private int? Id { get; set; }
    private DateTime? Date { get; set; }
    private int? TrainTemplateId { get; set; }
    private TimeSpan? Duration { get; set; }

    public TrainingBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public TrainingBuilder WithDate(DateTime date)
    {
        Date = date;

        return this;
    }

    public TrainingBuilder WithTrainTemplateId(int trainTemplateId)
    {
        TrainTemplateId = trainTemplateId;

        return this;
    }

    public TrainingBuilder WithDuration(TimeSpan duration)
    {
        Duration = duration;

        return this;
    }
    
    public Training Build()
    {
        return new Training
        {
            Date = Date ?? DateTime.Now,
            TrainTemplateId = TrainTemplateId ?? Faker.RandomNumber.Next(0, 100),
            Duration = Duration ?? TimeSpan.FromMinutes(Faker.RandomNumber.Next(20, 90)),
            Id = Id ?? Faker.RandomNumber.Next(0, 100)
        };
    }
}