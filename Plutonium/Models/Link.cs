using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plutonium.Classes;

namespace Plutonium.Models
{
    [Table("Links")]
    [Display(Name = "My Links")]
    public class Link
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        [CRUD(IsVisible = false)]
        public virtual int Id { get; set; }

        [Display(Name = "Label")]
        [CRUD(Ordering = 1)]
        public string Name { get; set; }

        [Display(Name = "Link Url")]
        [CRUD(Ordering = 2, IsVisible = true)]
        public string Url { get; set; }
    }
}