using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plutonium.Models
{
    [Table("Links")]
    public class Link
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
} 