namespace WebRunApplication.Domain.Entities;

public class Training
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int TrainTemplateId { get; set; }

    public TimeSpan Duration { get; set; }
}