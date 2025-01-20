using WebRunApplication.Domain.Entities;

namespace WebRunApplication.DbGenerator.Builders;

internal class IndicatorBuilder
{
    private int? Id { get; set; }
    private int? UserId { get; set; }
    private DateTime? Date { get; set; }
    private int? SystolicPressure { get; set; }
    private int? DiastolicPressure { get; set; }
    private TimeSpan? Duration { get; set; }
    private uint? Calories { get; set; }
    private double? AverageSpeed { get; set; }
    private uint? MinimumPulse { get; set; }
    private uint? AveragePulse { get; set; }
    private uint? MaximumPulse { get; set; }
    private uint? Steps { get; set; }

    public IndicatorBuilder WithId(int id)
    {
        Id = id;

        return this;
    }

    public IndicatorBuilder WithUserId(int userId)
    {
        UserId = userId;

        return this;
    }

    public IndicatorBuilder WithDate(DateTime date)
    {
        Date = date;

        return this;
    }

    public IndicatorBuilder WithSystolicPressure(int systolicPressure)
    {
        SystolicPressure = systolicPressure;

        return this;
    }

    public IndicatorBuilder WithDiastolicPressure(int diastolicPressure)
    {
        DiastolicPressure = diastolicPressure;

        return this;
    }

    public IndicatorBuilder WithDuration(TimeSpan duration)
    {
        Duration = duration;

        return this;
    }

    public IndicatorBuilder WithCalories(uint calories)
    {
        Calories = calories;
        
        return this;
    }

    public IndicatorBuilder WithAverageSpeed(double averageSpeed)
    {
        AverageSpeed = averageSpeed;

        return this;
    }

    public IndicatorBuilder WithMinimumPulse(uint minimumPulse)
    {
        MinimumPulse = minimumPulse;
        
        return this;
    }
    
    public IndicatorBuilder WithAveragePulse(uint averagePulse)
    {
        AveragePulse = averagePulse;
        
        return this;
    }
    
    public IndicatorBuilder WithMaximumPulse(uint maximumPulse)
    {
        MaximumPulse = maximumPulse;
        
        return this;
    }
    
    public IndicatorBuilder WithSteps(uint steps)
    {
        Steps = steps;
        
        return this;
    }
    
    public Indicator Build()
    {
        var minimumPulse = MinimumPulse ?? (uint)Faker.RandomNumber.Next(0, 230);
        var averagePulse = AveragePulse ?? (uint)Faker.RandomNumber.Next(minimumPulse, 230);
        
        return new Indicator
        {
            MinimumPulse = minimumPulse,
            AveragePulse = averagePulse,
            MaximumPulse = MaximumPulse ?? (uint)Faker.RandomNumber.Next(averagePulse, 230),
            AverageSpeed = AverageSpeed ?? (Random.Shared.NextDouble() + 0.5) * 10,
            Calories = Calories ?? (uint)Faker.RandomNumber.Next(200, 1000),
            Duration = Duration ?? TimeSpan.FromMinutes(Faker.RandomNumber.Next(5, 120)),
            Date = Date ?? DateTime.Now,
            Steps = Steps ?? (uint)Faker.RandomNumber.Next(1000, 20000),
            DiastolicPressure = DiastolicPressure,
            SystolicPressure = SystolicPressure,
            UserId = UserId ?? Faker.RandomNumber.Next(1, 100),
            Id = Id ?? Faker.RandomNumber.Next(1, 100)
        };
    }
}