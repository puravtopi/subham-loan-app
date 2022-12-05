using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class TenantNeedVM
    {
        public int TenantneedId { get; set; }

        public int TenantId { get; set; }
        public string Customer { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int? SectionPos { get; set; }
        public int? TaskGroupId { get; set; }
        public string TaskGroupName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? OwnId { get; set; }
        public string OwnName { get; set; }
        public string NeedEn { get; set; }
        public string NeedSp { get; set; }
        public bool? Coordinating { get; set; }
        public bool? Tracking { get; set; }
        public bool? _3rdparty { get; set; }
        public string DescEn { get; set; }
        public string DescSp { get; set; }
        public string Sample { get; set; }
        public string Video { get; set; }

        [ForeignKey("StatusId")]
        public virtual StatusVM Status { get; set; }

        [ForeignKey("TaskGroupId")]
        public virtual TaskGroupVM TaskGroup { get; set; }

        [ForeignKey("OwnId")]
        public virtual UserPersonaVM Own { get; set; }

        [ForeignKey("TenantId")]
        public virtual TenantMasterVM Tenant { get; set; }

        [ForeignKey("SectionId")]
        public virtual SectionVM Section { get; set; }

    }
}
