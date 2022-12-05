using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblLoanStage
    {
        public TblLoanStage()
        {
            TblLoanMsts = new HashSet<TblLoanMst>();
        }

        public int StageId { get; set; }
        public string StageName { get; set; }

        public virtual ICollection<TblLoanMst> TblLoanMsts { get; set; }
    }
}
