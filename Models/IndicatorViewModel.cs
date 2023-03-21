using System.Diagnostics.CodeAnalysis;

namespace WebRunApplication.Models
{
    public class IndicatorViewModel
    {
        public uint Calories { get; set; }

        public double AverageSpeed { get; set; }

        public uint MinimumPulse { get; set; }

        public uint AveragePulse { get; set; }

        public uint MaximumPulse { get; set; }

        public uint Steps { get; set; }
    }
}
