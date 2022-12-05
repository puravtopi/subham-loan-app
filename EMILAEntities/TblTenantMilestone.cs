using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblTenantMilestone
    {
        public TblTenantMilestone()
        {
            TblLoanMsts = new HashSet<TblLoanMst>();
        }

        public int TenantMilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public int? MilestonePos { get; set; }
        public int? TenantId { get; set; }

        public virtual TblTenantMaster Tenant { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMsts { get; set; }
    }
}
