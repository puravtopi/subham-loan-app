using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class TenantTasksVM
    {
        public int TenantTaskId { get; set; }
        public string Customer { get; set; }
        public int? TenantId { get; set; }
        public int? MilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public int? MilestonePos { get; set; }
        public int? TaskGroupId { get; set; }
        public string TaskGroupName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? OwnId { get; set; }
        public string OwnName { get; set; }
        public string TaskEn { get; set; }
        public string TaskSp { get; set; }
        public string Note { get; set; }

        [ForeignKey("MilestoneId")]
        public virtual MilestoneVM Milestone { get; set; }

        [ForeignKey("StatusId")]
        public virtual StatusVM Status { get; set; }

        [ForeignKey("TaskGroupId")]
        public virtual TaskGroupVM TaskGroup { get; set; }

        [ForeignKey("OwnId")]
        public virtual UserPersonaVM Own { get; set; }

        [ForeignKey("TenantId")]
        public virtual TenantMasterVM Tenant { get; set; }

    }
}
