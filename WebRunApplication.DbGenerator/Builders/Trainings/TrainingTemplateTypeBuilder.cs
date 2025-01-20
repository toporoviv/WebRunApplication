using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders.Trainings;

internal class TrainingTemplateTypeBuilder
{
    private int? Id { get; set; }
    private int? TemplateId { get; set; }
    private int? TrainingTypeId { get; set; }

    public TrainingTemplateTypeBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public TrainingTemplateTypeBuilder WithTemplateId(int templateId)
    {
        TemplateId = templateId;

        return this;
    }

    public TrainingTemplateTypeBuilder WithTrainingTypeId(int trainingTypeId)
    {
        TrainingTypeId = trainingTypeId;

        return this;
    }
    
    public TrainingTemplateType Build()
    {
        return new TrainingTemplateType
        {
            TrainingTypeId = TrainingTypeId ?? throw new ArgumentNullException(nameof(TrainingTypeId)),
            Id = Id ?? Faker.RandomNumber.Next(1, 100),
            TemplateId = TemplateId ?? throw new ArgumentNullException(nameof(TemplateId))
        };
    }
}