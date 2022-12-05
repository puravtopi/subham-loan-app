using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblSection
    {
        public TblSection()
        {
            TblLoanNeeds = new HashSet<TblLoanNeed>();
            TblNeedsDefalts = new HashSet<TblNeedsDefalt>();
            TblTenantNeeds = new HashSet<TblTenantNeed>();
        }

        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int? SectionPos { get; set; }

        public virtual ICollection<TblLoanNeed> TblLoanNeeds { get; set; }
        public virtual ICollection<TblNeedsDefalt> TblNeedsDefalts { get; set; }
        public virtual ICollection<TblTenantNeed> TblTenantNeeds { get; set; }
    }
}
