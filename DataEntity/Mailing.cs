namespace WebRunApplication.DataEntity
{
    public class Mailing
    {
        public uint Id { get; set; }

        public uint MailingTopicId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
