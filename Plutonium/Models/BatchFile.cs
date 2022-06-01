using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plutonium.Models
{
    [Table("BatchFiles")]
    public class BatchFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public virtual int Id { get; set; }

        [Column("FileName")]
        //[StringLength(12)]
        [Required(ErrorMessage = "FileName is required.")]

        public string FileName { get; set; }

        [Required(ErrorMessage = "Contents is required.")]
        public string Contents { get; set; }


    }
}