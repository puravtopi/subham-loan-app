using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblMilestone
    {
        public TblMilestone()
        {
            TblLoanTasks = new HashSet<TblLoanTask>();
            TblTasksDefalts = new HashSet<TblTasksDefalt>();
            TblTenantTasks = new HashSet<TblTenantTask>();
        }

        public int MilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public int? MilestonePos { get; set; }

        public virtual ICollection<TblLoanTask> TblLoanTasks { get; set; }
        public virtual ICollection<TblTasksDefalt> TblTasksDefalts { get; set; }
        public virtual ICollection<TblTenantTask> TblTenantTasks { get; set; }
    }
}
