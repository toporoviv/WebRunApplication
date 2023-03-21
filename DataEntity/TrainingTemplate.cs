using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebRunApplication.DataEntity
{
    public class TrainingTemplate
    {
        public uint Id { get; set; }

        public string Title { get; set; }
    }
}
