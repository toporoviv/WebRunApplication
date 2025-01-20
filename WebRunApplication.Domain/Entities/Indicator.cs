namespace WebRunApplication.Domain.Entities;

public class Indicator
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }
    
    public int? SystolicPressure { get; set; }
    
    public int? DiastolicPressure { get; set; }

    public TimeSpan Duration { get; set; }

    public uint Calories { get; set; }

    public double AverageSpeed { get; set; }

    public uint MinimumPulse { get; set; }

    public uint AveragePulse { get; set; }

    public uint MaximumPulse { get; set; }

    public uint Steps { get; set; }
}