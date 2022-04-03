using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plutonium.Models
{
    [Table("tblLookup")]
    public class CRUDLookup
    {
        [Key]
        public int id { get; set; }
        public string LookupName { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public int Ordering { get; set; }
        //  public bool IsActive { get; set; }

    }

}