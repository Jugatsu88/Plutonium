using Plutonium.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plutonium.ViewModels
{
    public class CrudViewModel
    {

        public string Title { get; set; }
        public string Url { get; set; }
        public string ModelName { get; set; }
        

        public CRUDModel CRUDModel { get; set; }
        public List<CRUDField> CRUDFields { get; set; }

        public string JsonFields
        {
            get
            {
                StringBuilder sb   = new StringBuilder();
                foreach (CRUDField c in CRUDFields.Where(x => x.Name != "Id"))
                {
                    sb.AppendLine(c.Name + ":");
                    sb.AppendLine("{");
                    sb.AppendLine("title: '" + c.Label + "'");
                    sb.AppendLine("},");
                }
                /*
            Address1:
                {
                title: 'Address1',
                        width: '15%'
                    },
                */
                return sb.ToString().TrimEnd(','); //.Replace(Environment.NewLine, " ");

            }
        }


        //   public List<CRUDLookup> CrudLookups { get; set; } 
    }
}
