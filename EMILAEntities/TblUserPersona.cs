using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblUserPersona
    {
        public TblUserPersona()
        {
            TblLoanNeeds = new HashSet<TblLoanNeed>();
            TblLoanTasks = new HashSet<TblLoanTask>();
            TblNeedsDefalts = new HashSet<TblNeedsDefalt>();
            TblTasksDefalts = new HashSet<TblTasksDefalt>();
            TblTenantNeeds = new HashSet<TblTenantNeed>();
            TblTenantTasks = new HashSet<TblTenantTask>();
        }

        public int PersonaId { get; set; }
        public int GroupId { get; set; }
        public string PersonaName { get; set; }
        public string PersonaShortname { get; set; }

        public virtual TblUserGroup Group { get; set; }
        public virtual ICollection<TblLoanNeed> TblLoanNeeds { get; set; }
        public virtual ICollection<TblLoanTask> TblLoanTasks { get; set; }
        public virtual ICollection<TblNeedsDefalt> TblNeedsDefalts { get; set; }
        public virtual ICollection<TblTasksDefalt> TblTasksDefalts { get; set; }
        public virtual ICollection<TblTenantNeed> TblTenantNeeds { get; set; }
        public virtual ICollection<TblTenantTask> TblTenantTasks { get; set; }
    }
}
