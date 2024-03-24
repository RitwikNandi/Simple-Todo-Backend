using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCRUD.Models
{
    public class DataEntries
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName ="nvarchar(199)")]
        public required string TaskName { get; set; }
    }
}
