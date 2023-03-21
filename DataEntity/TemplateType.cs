using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebRunApplication.DataEntity
{
    public class TemplateType
    {
        public uint Id { get; set; }

        public uint TemplateId { get; set; }

        public uint TrainingTypeId { get; set; }
    }
}
