using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanSubFolder
    {
        public TblLoanSubFolder()
        {
            TblLoanMsts = new HashSet<TblLoanMst>();
        }

        public int SubFolderId { get; set; }
        public string SubFolderName { get; set; }
        public int? MilestonePos { get; set; }

        public virtual ICollection<TblLoanMst> TblLoanMsts { get; set; }
    }
}
