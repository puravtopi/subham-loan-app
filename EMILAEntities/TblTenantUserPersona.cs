using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class TblTenantUserPersona
    {
        public TblTenantUserPersona()
        {
            TblUserMasters = new HashSet<TblUserMaster>();
        }

        public int TenantPersonaId { get; set; }
        public int GroupId { get; set; }
        public string PersonaName { get; set; }
        public string PersonaShortname { get; set; }
        public int? TenantId { get; set; }

        public virtual TblUserGroup Group { get; set; }
        public virtual TblTenantMaster Tenant { get; set; }
        public virtual ICollection<TblUserMaster> TblUserMasters { get; set; }
    }
}
