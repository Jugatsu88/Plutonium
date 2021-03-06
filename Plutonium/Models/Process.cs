using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plutonium.Models
{
 
    [Table("Processes")]
    public class Process
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public virtual int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }


    }
}