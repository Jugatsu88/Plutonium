using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plutonium.Models
{
    [Table("MenuItems")]
    public class MenuItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] // Identifier
        [Key]
        public virtual int Id { get; set; }

        [Column("Name")] // Label used in Menu
        [Required(ErrorMessage = "Name is required.")]

        public string Name { get; set; }

        public string Url { get; set; } // If Action Name is empty, one can provide a direct URL that the menu should point to.

        public int? ParentId { get; set; }
        public int Ordering { get; set; }
        //public bool IsVisible { get; set; }
        //public bool IsActive { get; set; }

    }
}