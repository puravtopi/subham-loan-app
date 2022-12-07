using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblTenantMaster
    {
        public TblTenantMaster()
        {
            TblBorrowerMsts = new HashSet<TblBorrowerMst>();
            TblCompanyMasters = new HashSet<TblCompanyMaster>();
            TblLoanMsts = new HashSet<TblLoanMst>();
            TblTenantMilestones = new HashSet<TblTenantMilestone>();
            TblTenantNeeds = new HashSet<TblTenantNeed>();
            TblTenantTasks = new HashSet<TblTenantTask>();
            TblTenantUserPersonas = new HashSet<TblTenantUserPersona>();
        }

        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public bool? IsTrial { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<TblBorrowerMst> TblBorrowerMsts { get; set; }
        public virtual ICollection<TblCompanyMaster> TblCompanyMasters { get; set; }
        public virtual ICollection<TblLoanMst> TblLoanMsts { get; set; }
        public virtual ICollection<TblTenantMilestone> TblTenantMilestones { get; set; }
        public virtual ICollection<TblTenantNeed> TblTenantNeeds { get; set; }
        public virtual ICollection<TblTenantTask> TblTenantTasks { get; set; }
        public virtual ICollection<TblTenantUserPersona> TblTenantUserPersonas { get; set; }
    }
}
