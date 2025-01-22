namespace WebRunApplication.Infrastructure.Models;

public sealed record Indicator
{
    public required int UserId { get; set; }
    public required DateTime Date { get; set; }
    public int? SystolicPressure { get; set; }
    public int? DiastolicPressure { get; set; }
    public required TimeSpan Duration { get; set; }
    public required uint Calories { get; set; }
    public required double AverageSpeed { get; set; }
    public required uint MinimumPulse { get; set; }
    public required uint AveragePulse { get; set; }
    public required uint MaximumPulse { get; set; }
    public required uint Steps { get; set; }
}