using Plutonium.Classes;
using System.Collections.Generic;

namespace Plutonium.ViewModels
{
    public class CrudViewModel
    {

        public string Title { get; set; }
        public string Url { get; set; }

        public CRUDModel CRUDModel { get; set; }
        public List<CRUDField> CRUDFields { get; set; }

     //   public List<CRUDLookup> CrudLookups { get; set; } 
    }
}
