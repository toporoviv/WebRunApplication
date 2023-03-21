using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebRunApplication.DataEntity
{
    public class Help
    {
        public uint Id { get; set; }

        public uint UserId { get; set; }

        public DateTime Date { get; set; }

        public string Question { get; set; }

        [MaybeNull()]
        public string? Answer { get; set; }
    }
}
