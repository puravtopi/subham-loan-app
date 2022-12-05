using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoanSubFolderVM
    {
        public int SubFolderId { get; set; }
        public int? FolderId { get; set; }
        public string SubFolderName { get; set; }
        public virtual LoanFolderVM Folder { get; set; }
    }
}
