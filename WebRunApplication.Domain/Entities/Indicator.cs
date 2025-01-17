namespace WebRunApplication.Domain.Entities;

public class Indicator
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }
        
    // todo: давление одним числом представить нельзя(
    public int? Pressure { get; set; }

    public TimeSpan Duration { get; set; }

    public uint Calories { get; set; }

    public double AverageSpeed { get; set; }

    public uint MinimumPulse { get; set; }

    public uint AveragePulse { get; set; }

    public uint MaximumPulse { get; set; }

    public uint Steps { get; set; }
}