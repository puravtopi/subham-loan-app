using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblTaskGroup
    {
        public TblTaskGroup()
        {
            TblLoanNeeds = new HashSet<TblLoanNeed>();
            TblLoanTasks = new HashSet<TblLoanTask>();
            TblNeedsDefalts = new HashSet<TblNeedsDefalt>();
            TblTasksDefalts = new HashSet<TblTasksDefalt>();
            TblTenantNeeds = new HashSet<TblTenantNeed>();
            TblTenantTasks = new HashSet<TblTenantTask>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<TblLoanNeed> TblLoanNeeds { get; set; }
        public virtual ICollection<TblLoanTask> TblLoanTasks { get; set; }
        public virtual ICollection<TblNeedsDefalt> TblNeedsDefalts { get; set; }
        public virtual ICollection<TblTasksDefalt> TblTasksDefalts { get; set; }
        public virtual ICollection<TblTenantNeed> TblTenantNeeds { get; set; }
        public virtual ICollection<TblTenantTask> TblTenantTasks { get; set; }
    }
}
