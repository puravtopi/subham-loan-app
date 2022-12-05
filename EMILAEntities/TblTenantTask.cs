using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblTenantTask
    {
        public int TenantTaskId { get; set; }
        public string Customer { get; set; }
        public int? TenantId { get; set; }
        public int? MilestoneId { get; set; }
        public int? MilestonePos { get; set; }
        public int? TaskGroupId { get; set; }
        public int? StatusId { get; set; }
        public int? OwnId { get; set; }
        public string TaskEn { get; set; }
        public string TaskSp { get; set; }
        public string Note { get; set; }

        public virtual TblMilestone Milestone { get; set; }
        public virtual TblUserPersona Own { get; set; }
        public virtual TblStatus Status { get; set; }
        public virtual TblTaskGroup TaskGroup { get; set; }
        public virtual TblTenantMaster Tenant { get; set; }
    }
}
