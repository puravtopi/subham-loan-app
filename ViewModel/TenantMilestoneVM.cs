using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class TenantMilestoneVM
    {
        public int TenantMilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public int? MilestonePos { get; set; }
        public int? TenantId { get; set; }

        public virtual TenantMasterVM Tenant { get; set; }
    }
}
