using Plutonium.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plutonium.Interfaces
{
    public interface IBatchFilesService
    {
        public List<BatchFileViewModel> GetItems();
        public string GetFileName(int BatchFileId);        
        public StringBuilder RunBatchFile(int BatchFileId);
    }
}
