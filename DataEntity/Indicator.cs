using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebRunApplication.DataEntity
{
    public class Indicator
    {
        public uint Id { get; set; } // primary key

        public uint UserId { get; set; }

        public DateTime Date { get; set; }

        [MaybeNull()]
        public uint? Pressure { get; set; }

        public TimeSpan Duration { get; set; }

        public uint Calories { get; set; }

        public double AverageSpeed { get; set; }

        public uint MinimumPulse { get; set; }

        public uint AveragePulse { get; set; }

        public uint MaximumPulse { get; set; }

        public uint Steps { get; set; }
    }
}
