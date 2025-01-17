namespace WebRunApplication.Domain.Entities;

public class TrainingType
{
    public int Id{ get; set; }

    public required string Title { get; set; }

    public string Description { get; set; }

    public uint MinimumPulse { get; set; }

    public uint MaximumPulse { get; set; }

    public TimeSpan Duration { get; set; }
}