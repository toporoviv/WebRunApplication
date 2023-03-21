namespace WebRunApplication.DataEntity
{
    public class Training
    {
        public uint Id { get; set; }

        public DateTime Date { get; set; }

        public uint TrainTemplateId { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
