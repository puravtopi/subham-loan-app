using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class LoanTasksVM
    {
        public int LoanTaskId { get; set; }
        public string Customer { get; set; }
        public int? LoanId { get; set; }
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
        public virtual MilestoneVM Milestone { get; set; }
        public virtual StatusVM Status { get; set; }
        public virtual TaskGroupVM TaskGroup { get; set; }
        public virtual UserPersonaVM Own { get; set; }


    }
}
