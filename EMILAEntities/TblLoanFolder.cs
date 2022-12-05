using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanFolder
    {
        public TblLoanFolder()
        {
            TblLoanMsts = new HashSet<TblLoanMst>();
        }

        public int FolderId { get; set; }
        public string FolderName { get; set; }

        public virtual ICollection<TblLoanMst> TblLoanMsts { get; set; }
    }
}
