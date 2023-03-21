using Microsoft.EntityFrameworkCore;

namespace WebRunApplication.DataEntity
{
    public class TrainingType
    {
        public uint Id{ get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public uint MinimumPulse { get; set; }

        public uint MaximumPulse { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
