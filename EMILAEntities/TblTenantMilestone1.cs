using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblTenantMilestone1
    {
        public int TmId { get; set; }
        public int? TenantId { get; set; }
        public int? StageId { get; set; }
        public string MilestoneName { get; set; }
        public string MilestoneLabel { get; set; }

        public virtual TblLoanStage Stage { get; set; }
    }
}
