using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plutonium.Models
{
    [Table("Buttons")]
    public class Button
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public virtual int Id { get; set; }

        [Column("Name")]
        //[StringLength(12)]
        [Required(ErrorMessage = "Name is required.")]

        public string Name { get; set; }

        public string PreUrl { get; set; }

        public string PostUrl { get; set; }

    }
}